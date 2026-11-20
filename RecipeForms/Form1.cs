// "using" = bring in a toolbox. This toolbox has the tools that talk to SQL Server.
using Microsoft.Data.SqlClient; // the tools that talk to SQL Server
using System.ComponentModel.DataAnnotations.Schema; //i have no clue where this came from
using System.Data; // DataTable, used to fill the grids
/*
 ======================= MAP OF THIS FILE =======================
 WHERE IS...                              GO TO (region, method)
 ----------------------------------------------------------------
 INSERT  into Users                       5. Profile tab        -> btnCreate_Click
 INSERT  into UserIngredient              6. My Ingredients tab -> btnAddIng_Click
 INSERT  into Rating (first rating)       8. Rate tab           -> btnRateSave_Click

 UPDATE  Users (edit profile)             5. Profile tab        -> btnSave_Click
 UPDATE  Rating (change a rating)         8. Rate tab           -> btnRateSave_Click

 DELETE  Users + Rating + UserIngredient  5. Profile tab        -> btnDelete_Click
 DELETE  UserIngredient                   6. My Ingredients tab -> btnRemoveIng_Click
 DELETE  Rating                           8. Rate tab           -> btnRateDelete_Click

 SEARCH  by email (log in)                5. Profile tab        -> btnLogin_Click
 SEARCH  ingredients (LIKE)               3. Refresh helpers    -> LoadAllIngredients
 SEARCH  recipes (LIKE + LEFT JOIN)       3. Refresh helpers    -> SearchRecipes
 SEARCH  recipes I can make               7. Find Recipes tab   -> btnFindMine_Click
         (3-table JOIN, GROUP BY, subquery)

 JOINs   UserIngredient + Ingredient      3. Refresh helpers    -> LoadMyIngredients
         Rating + Recipe                  3. Refresh helpers    -> LoadMyRatings
         RecipeIngredient + Ingredient    7. Find Recipes tab   -> btnViewRecipe_Click
         Recipe + RecipeIngredient
           + UserIngredient               7. Find Recipes tab   -> btnFindMine_Click

 TRANSACTION (all-or-nothing delete)      5. Profile tab        -> btnDelete_Click
 CONNECT to the database                  2. Database helpers   -> OpenConnection
 ----------------------------------------------------------------
 ================================================================
*/
namespace RecipeForms
{
    public partial class Form1 : Form
    {
        /*============================================================================================================================
        1. SETUP (Connection string, who is logged in, window startup
        ==============================================================================================================================
        */
        //the database's address and how to get in vvv
        const string ConnStr =
            "Server=SOUP\\SQLEXPRESS;Database=RecipeDB;Trusted_Connection=True;TrustServerCertificate=True;"; 
        //These two remember who is logged in
        int currentUserId = 0; //0 means 'nobody'
        string currentUserName = "";

        public Form1() //so this will only run once when the window is created
        {
            // Builds all the controls made in the Design tab
            InitializeComponent();
        }

        // Runs when the window finishes opening.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Show "Not logged in" at the top.
            ShowWho();
            //Fill the LEFT list with ingredients
            LoadAllIngredients("");
            // Show every recipe in the grid when the window opens.
            SearchRecipes("");
            // Fill the dropdown on the Rate tab.
            LoadRecipeChoices();
            // Refresh the "My ratings" table too.
            LoadMyRatings();
        }

       /*============================================================================================================================
       2. DATABASE HELPERS (used by many buttons)
       ==============================================================================================================================
       */

        // Makes a connection, opens it, and hands it back.
        private SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(ConnStr); // build the connection with our address
            conn.Open(); //This is the moment it really contacts SQL Server.
            return conn; // Hand the open connection to whoever asked.
        }

        // A helper that runs one DELETE command.
        // tx =  is the transaction: a way to say "do all my steps together, or none of them."
        private void RunDelete(SqlConnection conn, SqlTransaction tx, string sql, int id)
        {
            // Make the command, and attach it to the connection AND the transaction.
            using (SqlCommand cmd = new SqlCommand(sql, conn, tx)) //like your writing down the question
            {
                // Fill in the blank (@id) safely.
                cmd.Parameters.AddWithValue("@id", id); //fill in the blanks on the question
                // Run it. "NonQuery" means "do something, no rows come back."
                cmd.ExecuteNonQuery(); //ask it <-- how many rows changed
                //using(..) hang up automatically when done
            }
        }

        // Runs a command and shows ALL of its result rows in the grid on the Find Recipes tab.
        private void FillGrid(SqlCommand cmd)
        {
            using (SqlConnection conn = OpenConnection())
            {
                cmd.Connection = conn;

                // A DataAdapter is a "bucket filler": so it will run the command and then it
                // pours every result row into a DataTable 
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Show that table in the grid.
                    dgvRecipes.DataSource = table;
                }
            }
        }


        /*============================================================================================================================
        3. SCREEN REFRESH HELPERS (SELECTS that fill labels, list and grids)
        ==============================================================================================================================
        */
        // Looks at the sticky notes that remember who's logged in, writes that on the label
        //at the top of the window, and then refreshes the lists on the other tabs so they match
        private void ShowWho()
        {
            if (currentUserId == 0)
            {
                // Nobody is logged in.
                lblWho.Text = "Not logged in";
            }
            else
            {
                // Show the name and ID number.
                lblWho.Text = "Logged in as: " + currentUserName + " (ID " + currentUserId + ")";
            }
            LoadMyIngredients(); //refreshes the RIGHT list. It becomes empty if nobody logged in.
            LoadMyRatings(); // refresh the "My ratings" table too
        }


        // Fills the LEFT list with ingredients whose name CONTAINS the search words.
        //SEARCH BUTTON AND TEXTBOX IN MY INGREDIENTS TAB
        private void LoadAllIngredients(string search)
        {
            // Empty the list first so we don't pile up duplicates.
            lstAll.Items.Clear();

            try
            {
                using (SqlConnection conn = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(
                    //NAME LIKE: means exactly equal
                    //@pattern: means "contains these letters"
                    //ORDER BY Name: sorts A to Z
                    "SELECT Ingredient_ID, Name FROM Ingredient WHERE Name LIKE @pattern ORDER BY Name", conn))
                {
                    // The % signs mean "anything can be here". like "%chick%" matches "chicken breast".
                    // An empty search gives "%%", which matches EVERYTHING.
                    // THis makes it so if the box is empty , then the empty search shows the whole list
                    cmd.Parameters.AddWithValue("@pattern", "%" + search + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read() steps through the rows, one at a time.
                        while (reader.Read()) //used for lists of ingredients, and rating
                        {
                            // Make one IngredientItem (an ID plus a name) and add it to the list box
                            lstAll.Items.Add(new IngredientItem
                            //IngredientItem: the ID stays hidden inside the item
                            {
                                Id = reader.GetInt32(0),     // column 0 = Ingredient_ID
                                Name = reader.GetString(1)   // column 1 = Name
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load ingredients: " + ex.Message;
            }
        }

        // Fills the RIGHT list with the logged-in person's ingredients. JOIN!
        //A JOIN staples rows from two tables where the foreign key matches the primary key. UserIngredient only
        //has IDs, so I join to Ingredient to show names
        private void LoadMyIngredients()
        {
            lstMine.Items.Clear(); //is the right-hand list box on the My Ingredients tab, we empty it
            //always empty it first, so the old person's ingredients don't stay behind and the new ones don't pile on top


            // Nobody logged in? Leave the list empty.
            if (currentUserId == 0)
            {
                return; //why the right list goes blank when you log out
            }

            try
            {
                using (SqlConnection conn = OpenConnection())
                //------------------ UserIngredient only holds ID numbers. This JOIN matches its Ingredient_ID, which is a foreign key, to the primary key in the Ingredient table, so I can show ingredient names. The WHERE limits it to the logged-in user, and ORDER BY sorts it alphabetically

                //THE RIGHT LIST = JOIN( LoadMyIngredeints)
                // UserIngredient only holds ID numbers (who has what).
                //ex)1,17 -> person 1 has food 17. NOT READABLE
                // JOIN looks up each ingredient's NAME in the Ingredient table.
                // Result: the number 17 becomes the word "eggs"
                //ui is just an alias
                //SO BASICALLY JOIN ,staple each of those cards to the card in the Ingredeint who ID number is the same
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT i.Ingredient_ID, i.Name
                    FROM UserIngredient ui
                    JOIN Ingredient i ON i.Ingredient_ID = ui.Ingredient_ID 
                    WHERE ui.User_ID = @id
                    ORDER BY i.Name", conn))
                {
                    cmd.Parameters.AddWithValue("@id", currentUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstMine.Items.Add(new IngredientItem
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load your ingredients: " + ex.Message;
            }
        }



        // [Find Recipes tab] SEARCH: recipes by title or cuisine, with each recipe's average stars (LIKE + LEFT JOIN).
        private void SearchRecipes(string text)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT r.Recipe_ID, r.Title, r.Cuisine, r.Difficulty,
                     AVG(CAST(rt.Stars AS FLOAT)) AS AvgStars,
                     COUNT(rt.Rating_ID) AS Ratings
                    FROM Recipe r
                    LEFT JOIN Rating rt ON rt.Recipe_ID = r.Recipe_ID
                    WHERE r.Title LIKE @p OR r.Cuisine LIKE @p
                    GROUP BY r.Recipe_ID, r.Title, r.Cuisine, r.Difficulty
                    ORDER BY r.Title"))
                {
                    // % means "anything can be here", so %mex% matches "Mexican".
                    // An empty search gives "%%", which matches EVERY recipe.
                    cmd.Parameters.AddWithValue("@p", "%" + text + "%");
                    FillGrid(cmd);
                }

                lblStatus.Text = dgvRecipes.Rows.Count + " recipe(s) shown.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not search recipes: " + ex.Message;
            }
        }
        // [Rate tab] SELECT: fills the dropdown with every recipe title.
        private void LoadRecipeChoices()
        {
            cboRecipe.Items.Clear();

            try
            {
                using (SqlConnection conn = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Recipe_ID, Title FROM Recipe ORDER BY Title", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // One RecipeItem (an ID plus a title) goes into the dropdown.
                        cboRecipe.Items.Add(new RecipeItem
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1)
                        });
                    }
                }

                // Pick the first recipe so the dropdown isn't blank.
                if (cboRecipe.Items.Count > 0)
                {
                    cboRecipe.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load recipes: " + ex.Message;
            }
        }

        // [Rate tab] JOIN: shows the logged-in person's ratings in the grid.
        private void LoadMyRatings()
        {
            // Nobody logged in? Empty the grid.
            if (currentUserId == 0)
            {
                dgvMyRatings.DataSource = null;
                return;
            }

            try
            {
                using (SqlConnection conn = OpenConnection())
                // Rating only has ID numbers. JOIN looks up each recipe's TITLE.
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT r.Title, rt.Stars
                    FROM Rating rt
                    JOIN Recipe r ON r.Recipe_ID = rt.Recipe_ID
                    WHERE rt.User_ID = @id
                    ORDER BY r.Title", conn))
                {
                    cmd.Parameters.AddWithValue("@id", currentUserId);

                    // The adapter runs the command and pours the rows into a DataTable.
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvMyRatings.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load your ratings: " + ex.Message;
            }
        }

        /*============================================================================================================================
        4. TEST BUTTON
        ==============================================================================================================================
        */

        // [Test] SELECT COUNT(*) to check the connection.
        private void btnTest_Click(object sender, EventArgs e)
        {
            // Try this. If anything goes wrong, jump down to "catch".
            try
            {
                // Use our helper to get an open connection.
                using (SqlConnection conn = OpenConnection())
                {
                    // The question we want to ask: how many recipes are there?
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Recipe", conn))
                    {
                        // get back ONE value (the count).
                        int count = (int)cmd.ExecuteScalar(); //RxecuteScolor = one single value

                        lblStatus.Text = "Connected! Recipes in the database: " + count;
                    }
                }
            }
            catch (Exception ex)
            {
                // Show the error on the label instead of crashing.
                lblStatus.Text = "Could not connect: " + ex.Message;
            }
        }
        /*============================================================================================================================
        5. TEST CONNECTION BUTTONS [PROFILE]
        ==============================================================================================================================
        */
        // [Profile tab] INSERT into Users.
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string first = txtFirst.Text.Trim(); //reads what was typed in the box
            string last = txtLast.Text.Trim();// <--(Trim): cuts off extra spaces
            string email = txtEmail.Text.Trim();

            // Means "if any box is empty"
            if (first == "" || last == "" || email == "")
            {
                lblStatus.Text = "Please fill in first name, last name and email."; //shows message
                return; // stop here
            }

            // if the email has no @ sign
            if (!email.Contains("@"))
            {
                lblStatus.Text = "That doesn't look like an email.";
                return;
            }

            try
            {
                //CHECK EMAIL IN DATABASE!!!
                using (SqlConnection conn = OpenConnection())
                {
                    using (SqlCommand check = new SqlCommand(
                        //SELECT COUNT(*) FROM Users Where Email = @email
                        //ASKS "how many people already have this email?"
                        "SELECT COUNT(*) FROM Users WHERE Email = @email", conn))
                    {
                        check.Parameters.AddWithValue("@email", email);
                        int already = (int)check.ExecuteScalar();
                        if (already > 0) //if it's more than 0, the program stops, so two people can't share the same email.
                        {
                            lblStatus.Text = "That email is already used. Try 'Log in with email'.";
                            return;
                        }
                    }

                    //INSERT INTO Users: "Put a new card in the Users drawer"
                    //(Name, LastName, Email): "I'll fill in these three boxes"
                    //VALUES (@first,@last,@email): "with these three values, in the same order"
                    //(no User_ID listed): SQL numbers the card itself
                    //OUTPUT INSERTED.UserID: "Tell me the number you just gave it"
                    using (SqlCommand ins = new SqlCommand(
                        "INSERT INTO Users (Name, LastName, Email) OUTPUT INSERTED.User_ID VALUES (@first, @last, @email)", conn))
                    {
                        ins.Parameters.AddWithValue("@first", first);
                        ins.Parameters.AddWithValue("@last", last);
                        ins.Parameters.AddWithValue("@email", email);

                        // ExecuteScalar catches the one value that came back: the new ID.
                        currentUserId = (int)ins.ExecuteScalar(); //now we know who's logged in
                        currentUserName = first;
                    }
                }

                ShowWho(); //updates the top label, and the status line says....
                lblStatus.Text = "Profile created! Your ID number is " + currentUserId;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not create profile: " + ex.Message;
            }
        }

        // [Profile tab] SEARCH Users by email (this is the log in).
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Only the email box is needed to log in.
            string email = txtEmail.Text.Trim();

            if (email == "")
            {
                lblStatus.Text = "Type your email in the Email box first.";
                return;
            }

            try
            {
                //Login with email = SEARCH (btnLogin_Click)
                using (SqlConnection conn = OpenConnection())
                using (SqlCommand cmd = new SqlCommand(
                    //Find the person who owns this email
                    //SELECT: show me
                    //WHERE: filters
                    //so a search is a SELECT with a WHERE filter. Equals means an exact match.
                    "SELECT User_ID, Name, LastName FROM Users WHERE Email = @email", conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read() moves to the first row. 
                        if (reader.Read()) //TRUE: found a match!
                        {
                            // Column 0 = User_ID, column 1 = Name, column 2 = LastName.
                            currentUserId = reader.GetInt32(0); //read column,0,1
                            currentUserName = reader.GetString(1);

                            // Fill in the boxes with what's saved in the database.
                            txtFirst.Text = currentUserName;
                            // if LastName is empty in the database,
                            //use empty text, or otherwise read it.
                            //why:???? oh bc it crashes
                            txtLast.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);

                            ShowWho(); //this will update the bottom label
                            //then calls LoadMyIngredients >>>updates all the tabs
                            //then calls LoadMyRatings
                            lblStatus.Text = "Welcome back, " + currentUserName + "!";
                        }
                        else //if nothing matched!
                        {
                            lblStatus.Text = "No profile found with that email.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not log in: " + ex.Message;
            }
        }
        // [Profile tab] UPDATE Users(this is the edit).
        // Save changes = EDIT (btnSave_Click)
        //CREATE PROFILE BUTTON/
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Means nobody's logged in, soo..
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first.";
                return; //stop here and leave -->check it works
                //cause you can only edit someone who exists
            }

            string first = txtFirst.Text.Trim();
            string last = txtLast.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (first == "" || last == "" || email == "")
            {
                lblStatus.Text = "Please fill in first name, last name and email.";
                return;
            }
            if (!email.Contains("@"))
            {
                lblStatus.Text = "That doesn't look like an email.";
                return;
            }

            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    // SEARCH: is this email used by SOMEONE ELSE? 
                    using (SqlCommand check = new SqlCommand(
                        //without it, saving your own unchanged email would look like a duplicate
                        "SELECT COUNT(*) FROM Users WHERE Email = @email AND User_ID <> @id", conn))
                    {
                        check.Parameters.AddWithValue("@email", email);
                        check.Parameters.AddWithValue("@id", currentUserId);
                        if ((int)check.ExecuteScalar() > 0)
                        {
                            lblStatus.Text = "Another profile already uses that email.";
                            return;
                        }
                    }

                    // UPDATE USERS: change the columns of ONE row.
                    // SET = rewrite these boxes with these new value in this case NAME
                    //WHERE User_ID = @id means "only the card with thid ID.
                    //ok so like without the WHERE, erm everything would be rewritten, learned that the hard way
                    using (SqlCommand upd = new SqlCommand(
                        "UPDATE Users SET Name = @first, LastName = @last, Email = @email WHERE User_ID = @id", conn)) //@id is a blank line on a form
                    {
                        upd.Parameters.AddWithValue("@first", first);
                        upd.Parameters.AddWithValue("@last", last);
                        upd.Parameters.AddWithValue("@email", email);
                        upd.Parameters.AddWithValue("@id", currentUserId); //this fills in the @id
                        //^^we do this bc a sneaky person could type SQL commands instead of a name <<--SQL injection
                        //we stop it by sending the value separately

                        // returns how many rows changed, which should be 1
                        int rows = upd.ExecuteNonQuery();

                        currentUserName = first;
                        ShowWho();
                        lblStatus.Text = "Saved! (" + rows + " row updated)";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not save: " + ex.Message;
            }
        }

        // [Profile tab] DELETE from Rating, UserIngredient and Users (one transaction).
        //Delete my profile = DELETE (btnDelete_Click)
        //throw away my card, and everything that points to me
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentUserId == 0) //if no body is logged in,stop
            {
                lblStatus.Text = "Log in first.";
                return;
            }

            // Ask "are you sure?" in a pop-up window. This is a safety net.
            DialogResult answer = MessageBox.Show(
                "Delete your profile, your ingredients and your ratings? This cannot be undone.",
                "Confirm delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // If they didn't click Yes, stop.
            if (answer != DialogResult.Yes)
            {
                return;
            }

            //Delete uses a transaction and removes the child tables first, because of the foreign keys
            try
            {
                using (SqlConnection conn = OpenConnection())
                {
                    // A transaction means ALL three deletes happen, or NONE do.
                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        // CHILD tables first! Rating and UserIngredient point at Users,
                        // so SQL Server won't let us delete the user while they still exist.
                        //what the SQL MEANS:
                            //DELETE FROM Users: throw away cards from this drawer
                            //WHERE: "only this card"  
                        RunDelete(conn, tx, "DELETE FROM Rating WHERE User_ID = @id", currentUserId); //using our helper RunDelete #2
                        RunDelete(conn, tx, "DELETE FROM UserIngredient WHERE User_ID = @id", currentUserId);

                        // Now the PARENT row can go.
                        RunDelete(conn, tx, "DELETE FROM Users WHERE User_ID = @id", currentUserId);

                        // Make it all permanent.
                        tx.Commit();
                    }
                }
                //RESET
                // Forget who was logged in and clear the three boxes.
                currentUserId = 0;
                currentUserName = "";
                txtFirst.Text = "";
                txtLast.Text = "";
                txtEmail.Text = "";
                ShowWho(); //delete form the other tabs
                lblStatus.Text = "Profile deleted.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not delete: " + ex.Message;
            }
        }

        // [Profile tab] no SQL!!!!: just forgets who is logged in.
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Just forget who was logged in. Nothing in the database changes.
            currentUserId = 0;
            currentUserName = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtEmail.Text = "";
            ShowWho();
            lblStatus.Text = "Logged out.";
        }

        /*============================================================================================================================
        6. MY INGREDIENTS TAB (UserIngredient table: SEARCH, INSERT, DELETE
        ==============================================================================================================================
        */
        // [My Ingredients tab] SEARCH ingredients (LIKE).
        private void btnIngSearch_Click(object sender, EventArgs e)
        {
            // Take the words from the search box (Trim cuts off extra spaces) and refill the left list.
            LoadAllIngredients(txtIngSearch.Text.Trim());

            // Tell the user how many were found.
            lblStatus.Text = lstAll.Items.Count + " ingredient(s) found."; //when SEARCH button
        }


        // [My Ingredients tab] INSERT into UserIngredient.
        private void btnAddIng_Click(object sender, EventArgs e)
        {
            // You must be logged in to have a kitchen.
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first (Profile tab).";
                return;
            }

            // lstAll.SelectedItem as IngredientItem means
            //"which row did you click on the left? Treat it as an ingredient."
            //if nothing was click, picked is null, and it says ..
            IngredientItem picked = lstAll.SelectedItem as IngredientItem;
            if (picked == null)
            {
                lblStatus.Text = "Click an ingredient in the LEFT list first.";
                return;
            }

            try //INSERT into the connector table, after a SELECT checks it isn't a duplicate
            {
                using (SqlConnection conn = OpenConnection())
                {
                    // SEARCH FIRST: does this person already have it?
                    using (SqlCommand check = new SqlCommand(
                        "SELECT COUNT(*) FROM UserIngredient WHERE User_ID = @u AND Ingredient_ID = @i", conn))
                    {
                        check.Parameters.AddWithValue("@u", currentUserId);
                        check.Parameters.AddWithValue("@i", picked.Id);

                        if ((int)check.ExecuteScalar() > 0) //if the count if more than zero
                        {
                            lblStatus.Text = "You already have " + picked.Name + ".";
                            return;
                            //this works bc the table's composite primary key (User_ID, Ingredient_ID)
                            //wouldn't allow the same pair twice anyway, andd checking first gives a friendly message.
                        }
                    }

                    // INSERT one new row: "this user has this ingredient".
                    using (SqlCommand ins = new SqlCommand(
                        //this person has this food
                        "INSERT INTO UserIngredient (User_ID, Ingredient_ID) VALUES (@u, @i)", conn))
                    {
                        ins.Parameters.AddWithValue("@u", currentUserId);
                        ins.Parameters.AddWithValue("@i", picked.Id); //hidden ID number (who and which food)
                        ins.ExecuteNonQuery();   // runs it, no rows come back
                    }
                }

                // Refresh the right list so the new ingredient shows up.
                LoadMyIngredients();
                lblStatus.Text = "Added " + picked.Name + ".";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not add: " + ex.Message;
            }
        }
        /// [My Ingredients tab] DELETE from UserIngredient.
        /// Remove selected = DELETE
        private void btnRemoveIng_Click(object sender, EventArgs e)
        {
            //check you're logged in
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first (Profile tab).";
                return;
            }

            // Which ingredient is selected in the RIGHT list?
            //check you clicked something on the RIGHT list
            IngredientItem picked = lstMine.SelectedItem as IngredientItem;
            if (picked == null)
            {
                lblStatus.Text = "Click an ingredient in the RIGHT list first.";
                return;
            }

            try //Delete removes one row, bc the WHERE names both the user and the ingredient
            {
                using (SqlConnection conn = OpenConnection())
                // DELETE removes ONE row: this person's link to this ingredient.
                // WHERE is super important. Without it, EVERY row in the table would be deleted!
                using (SqlCommand del = new SqlCommand(
                    "DELETE FROM UserIngredient WHERE User_ID = @u AND Ingredient_ID = @i", conn))
                {
                    del.Parameters.AddWithValue("@u", currentUserId);
                    del.Parameters.AddWithValue("@i", picked.Id);

                    // ExecuteNonQuery tells us how many rows were deleted (should be 1).
                    int rows = del.ExecuteNonQuery();

                    // Refresh the right list so the ingredient disappears.
                    LoadMyIngredients();
                    lblStatus.Text = "Removed " + picked.Name + " (" + rows + " row deleted).";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not remove: " + ex.Message;
            }
        }



        /*============================================================================================================================
        7. FIND RECIPES TAB (Recipe, RecipeIngredient: SEARCH and JOINs)
        ==============================================================================================================================
        */
        // [Find Recipes tab] SEARCH recipes (LIKE + LEFT JOIN, see SearchRecipes in region 3).
        private void btnRecipeSearch_Click(object sender, EventArgs e)
        {
            // Take the words from the box and search.
            SearchRecipes(txtRecipeSearch.Text.Trim());
        }

        // [Find Recipes tab] SEARCH recipes I can make (3-table JOIN, GROUP BY, subquery).
        private void btnFindMine_Click(object sender, EventArgs e)
        {
            // You need a kitchen to find recipes for.
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first (Profile tab).";
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                 @"SELECT r.Recipe_ID, r.Title, r.Cuisine, r.Difficulty,
                 COUNT(*) AS YouHave,
                 (SELECT COUNT(*) FROM RecipeIngredient x WHERE x.Recipe_ID = r.Recipe_ID) AS Needed
                 FROM Recipe r
                 JOIN RecipeIngredient ri ON ri.Recipe_ID = r.Recipe_ID
                 JOIN UserIngredient ui   ON ui.Ingredient_ID = ri.Ingredient_ID
                 WHERE ui.User_ID = @id
                 GROUP BY r.Recipe_ID, r.Title, r.Cuisine, r.Difficulty
                 ORDER BY YouHave DESC, r.Title"))
                {
                    cmd.Parameters.AddWithValue("@id", currentUserId);
                    FillGrid(cmd);
                }

                if (dgvRecipes.Rows.Count == 0)
                {
                    lblStatus.Text = "No recipes match yet. Add ingredients on the My Ingredients tab.";
                }
                else
                {
                    lblStatus.Text = dgvRecipes.Rows.Count + " recipe(s) you can start on. Best match first.";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not find recipes: " + ex.Message;
            }
        }

        private void btnViewRecipe_Click(object sender, EventArgs e)
        {
            // Did the user click a row in the grid?
            // CurrentRow is the row that's highlighted (null if there is none).
            if (dgvRecipes.CurrentRow == null)
            {
                lblStatus.Text = "Click a recipe row in the table first.";
                return;
            }

            // Every row in the grid has a Recipe_ID column (it's in both of our queries).
            // Read the ID from the highlighted row and turn it into a whole number.
            int recipeId = Convert.ToInt32(dgvRecipes.CurrentRow.Cells["Recipe_ID"].Value);

            try
            {
                string text = "";            // we build the box's words in here, piece by piece
                string instructions = "";    // we save the instructions to add at the end

                using (SqlConnection conn = OpenConnection())
                {
                    // ---------- 1. The recipe itself (SELECT ... WHERE) ----------
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT Title, Cuisine, Difficulty, Instructions FROM Recipe WHERE Recipe_ID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", recipeId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read() is false if no row matched.
                            if (!reader.Read())
                            {
                                lblStatus.Text = "Recipe not found.";
                                return;
                            }

                            // Title in capitals, then cuisine and difficulty on the next line.
                            // IsDBNull checks for empty values so GetString doesn't crash.
                            text += reader.GetString(0).ToUpper() + Environment.NewLine;
                            text += (reader.IsDBNull(1) ? "" : reader.GetString(1)) + " - "
                                  + (reader.IsDBNull(2) ? "" : reader.GetString(2))
                                  + Environment.NewLine + Environment.NewLine;

                            instructions = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        }
                    }

                    // ---------- 2. Its ingredients (a JOIN!) ----------
                    text += "INGREDIENTS" + Environment.NewLine;

                    // RecipeIngredient only has ID numbers and quantities.
                    // JOIN looks up each ingredient's NAME in the Ingredient table.
                    using (SqlCommand cmd = new SqlCommand(
                        @"SELECT i.Name, ri.Quantity
              FROM RecipeIngredient ri
              JOIN Ingredient i ON i.Ingredient_ID = ri.Ingredient_ID
              WHERE ri.Recipe_ID = @id
              ORDER BY i.Name", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", recipeId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // One line per ingredient: "  - 1 1/2 cups flour"
                            while (reader.Read())
                            {
                                string quantity = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                text += "  - " + quantity + " " + reader.GetString(0) + Environment.NewLine;
                            }
                        }
                    }
                }

                // ---------- 3. Add the instructions at the bottom ----------
                text += Environment.NewLine + "INSTRUCTIONS" + Environment.NewLine + instructions;

                // Show everything in the big box.
                txtDetails.Text = text;
                lblStatus.Text = "Showing recipe details.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not load the recipe: " + ex.Message;
            }
        }
        /*============================================================================================================================
        8. RATE A RECIPE TAB (Rating table: UPDATE or INSERT,UPDATE DELETE
        ==============================================================================================================================
        */
        // [Rate tab] UPDATE Rating, or INSERT if it is the first rating.
        private void btnRateSave_Click(object sender, EventArgs e)
        {
            // You must be logged in to rate.
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first (Profile tab).";
                return;
            }

            // Which recipe is chosen in the dropdown? (null if none)
            RecipeItem picked = cboRecipe.SelectedItem as RecipeItem;
            if (picked == null)
            {
                lblStatus.Text = "Choose a recipe first.";
                return;
            }

            // The number box holds a decimal, so turn it into a whole number.
            int stars = (int)nudStars.Value;

            try
            {
                string message;

                using (SqlConnection conn = OpenConnection())
                {
                    // STEP 1: try to UPDATE this person's old rating for this recipe.
                    // SET = what to change. WHERE = which row.
                    int changed;
                    using (SqlCommand upd = new SqlCommand(
                        "UPDATE Rating SET Stars = @s WHERE User_ID = @u AND Recipe_ID = @r", conn))
                    {
                        upd.Parameters.AddWithValue("@s", stars);
                        upd.Parameters.AddWithValue("@u", currentUserId);
                        upd.Parameters.AddWithValue("@r", picked.Id);

                        // ExecuteNonQuery tells us how many rows changed.
                        changed = upd.ExecuteNonQuery();
                    }

                    if (changed > 0)
                    {
                        // An old rating existed and was changed. That was the edit.
                        message = "Updated your rating for " + picked.Title + " to " + stars + " stars.";
                    }
                    else
                    {
                        // STEP 2: nothing was changed, so this is a FIRST rating. INSERT a new row.
                        // We skip Rating_ID because SQL Server numbers it (IDENTITY).
                        using (SqlCommand ins = new SqlCommand(
                            "INSERT INTO Rating (User_ID, Recipe_ID, Stars) VALUES (@u, @r, @s)", conn))
                        {
                            ins.Parameters.AddWithValue("@u", currentUserId);
                            ins.Parameters.AddWithValue("@r", picked.Id);
                            ins.Parameters.AddWithValue("@s", stars);
                            ins.ExecuteNonQuery();
                        }

                        message = "Saved your " + stars + "-star rating for " + picked.Title + ".";
                    }
                }

                // Refresh the grid so the change shows up.
                LoadMyRatings();
                lblStatus.Text = message;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not save rating: " + ex.Message;
            }
        }

        // [Rate tab] DELETE from Rating.
        private void btnRateDelete_Click(object sender, EventArgs e)
        {
            if (currentUserId == 0)
            {
                lblStatus.Text = "Log in first (Profile tab).";
                return;
            }

            RecipeItem picked = cboRecipe.SelectedItem as RecipeItem;
            if (picked == null)
            {
                lblStatus.Text = "Choose a recipe first.";
                return;
            }

            try
            {
                int rows;

                using (SqlConnection conn = OpenConnection())
                // DELETE removes ONE row: this person's rating of this recipe.
                // Without WHERE, EVERY rating in the table would be deleted!
                using (SqlCommand del = new SqlCommand(
                    "DELETE FROM Rating WHERE User_ID = @u AND Recipe_ID = @r", conn))
                {
                    del.Parameters.AddWithValue("@u", currentUserId);
                    del.Parameters.AddWithValue("@r", picked.Id);
                    rows = del.ExecuteNonQuery();
                }

                // Refresh the grid so the rating disappears.
                LoadMyRatings();

                if (rows == 0)
                {
                    lblStatus.Text = "You haven't rated " + picked.Title + " yet.";
                }
                else
                {
                    lblStatus.Text = "Deleted your rating for " + picked.Title + " (" + rows + " row deleted).";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Could not delete rating: " + ex.Message;
            }
        }

        /*============================================================================================================================
        9. SMALL HELPER CLASSES (an ID plus a name)
        ==============================================================================================================================
        */
        // A tiny container: one ingredient = an ID number plus a name.
        // The list box shows whatever ToString() gives back, here just the name.
        // But we still keep the ID hidden inside, so we know WHICH ingredient was clicked.
        public class IngredientItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";

            public override string ToString()
            {
                return Name;
            }
        }

        // A tiny container: one recipe = an ID number plus a title.
        // The dropdown shows the title (ToString), but the ID stays hidden inside.
        public class RecipeItem
        {
            public int Id { get; set; }
            public string Title { get; set; } = "";

            public override string ToString()
            {
                return Title;
            }
        }

    }
}