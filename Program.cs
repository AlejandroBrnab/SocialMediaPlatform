using SocialMedia;
using System;

//Intanstiating the SocialMediaPlatform class
SocialMediaPlatform socialMedia = new SocialMediaPlatform();

//Calling the SeekFakeData and then after printing all the user in the platform
socialMedia.SeekFakeData();
Console.WriteLine("The users in the platform are: ");
socialMedia.DisplayAllUsers();

//Here the posts of some users are being displayed
Console.WriteLine("\nAll Posts:");
socialMedia.DisplayAllPosts();

//Here it will be a search by content
Console.WriteLine("\nSearch a post by content:\n");
//inputting a word that can be found in any of the posts
socialMedia.SearchPostsByContent("pic.");
//inputting a word that is not available in any of the posts
socialMedia.SearchPostsByContent("hola");

//searching a user by their username
Console.WriteLine("\nSearch a person by their username:\n");
//inputting a username that exists
socialMedia.SearchByUsername("Alice");
//inputting a username that does not exist
socialMedia.SearchByUsername("Bartolomeo");


//UserProfile class
public class UserProfile
{
    public string Username { get; private set;}
    public List<Post> Posts { get; private set; } = new List<Post>();
    public List<UserProfile> Following { get; private set; } = new List<UserProfile>();

    public bool IsVerified { get; private set; }

    public UserProfile(string username)
    {
        Username = username;
    }

    public void AddPost(Post post)
    {
        Posts.Add(post);
    }

    public void DisplayPosts()
    {
        foreach (var post in Posts)
        {
            Console.WriteLine(post.Content);
        }
    }

    public void Follow(UserProfile user)
    {
        Following.Add(user);
    }

    public void DisplayFollowing()
    {
        foreach(var user in Following)
        {
            Console.WriteLine(user.Username);
        }
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public string DisplayUsername()
    {
        return IsVerified ? $"{Username} [verified]" : Username;
    }

    public void DisplayFeed()
    {
        var feedPosts = Following.SelectMany(u => u.Posts).OrderByDescending(p => p.TimeStamp).ToList();
        foreach(var post in feedPosts)
        {
            Console.WriteLine($"{post.Content} - {post.TimeStamp}");
        }
    }
}

//SocialMediaPlatform class
public class SocialMediaPlatform
{
    public List<UserProfile> Users { get; private set; } = new List<UserProfile>();

    public void RegisterUser(UserProfile username)
    {
        Users.Add(username);
    }

    public UserProfile GetUser(string username)
    {
        return Users.FirstOrDefault(u => u.Username == username);
    }

    public void DisplayAllPosts()
    {
        foreach (var user in Users)
        {
            user.DisplayPosts();
        }
    }

    public void DisplayAllUsers()
    {
        foreach (var user in Users)
        {
            Console.WriteLine(user.DisplayUsername());
        }
    }

    //The added feature is here. The feature added is the search functionality

    //This method searches the posts based on their content. 
    public void SearchPostsByContent(string contentToSearch)
    {
        bool postFound = false;
        foreach (var user in Users) //this line of code iterates through the users List
        {
            foreach (var post in user.Posts) //this line iterates through the user's posts
            {
                if (post.Content.Split(' ').Contains(contentToSearch)) //this line of code checks if the input of the user matches any word from any of the posts.
                {
                    Console.WriteLine($"{post.Content} [by the user {user.Username}]");
                    postFound = true;
                }
            }
        }
        if (!postFound)
        {
            Console.WriteLine("Post could not be found"); //this line of code is dipslayed if the post is not available in the List of posts.
            return;
        }
    }

    //This method searches a user based on their username
    public void SearchByUsername(string usernameToSearch)
    {
        bool userFound = false;
        foreach (var user in Users)
        {
            if (user.Username == usernameToSearch)
            {
                Console.WriteLine($"You found the user: {user.Username}"); //this line of code is displayed when the user matches with the input username
                userFound = true;
            }
        }
        if(!userFound)
        {
            Console.WriteLine($"The username {usernameToSearch} does not exist."); //this line of code is displayed if the user does not exist in
            return;                                                                //the List of users
        }
    }

    public void SeekFakeData()
    {
        //creating the users
        UserProfile alice = new UserProfile("Alice");
        UserProfile bob = new UserProfile("Bob");
        UserProfile charlie = new UserProfile("Charlie");
        UserProfile dave = new UserProfile("Dave");

        //registering users
        RegisterUser(alice);
        RegisterUser(bob);
        RegisterUser(charlie);
        RegisterUser(dave);

        //add posts for Alice
        alice.AddPost(new TextPost { Content = "Hello World!" });
        alice.AddPost(new ImagePost { Content = "Check out this cool pic.", ImageUrl = "http:// example" });

        //Add posts for Bob
        bob.AddPost(new TextPost { Content = "Having a great day!"});
        bob.AddPost(new ImagePost { Content = "My lunch today", ImageUrl = "http:// example.com" });

        //dave follows Bob and Alice
        dave.Follow(bob);
        dave.Follow(alice);

        //Users interactg with posts
        alice.Posts[0].LikePost();
        bob.Posts[1].React(Post.ReactionType.Love);

        //verify a user
        alice.Verify();
    }
}