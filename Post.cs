using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia
{
    //Post class
    public class Post
    {
        public string Content { get; set; }
        public DateTime TimeStamp { get; private set; } = DateTime.Now;

        public int Likes { get; private set; }
        public enum ReactionType { Like, Love, Haha, Sad, Angry }
        public Dictionary<ReactionType, int> Reactions { get; private set; } = new Dictionary<ReactionType, int>();

        public List<Comment> Comments { get; private set; } = new List<Comment> ();

        public void LikePost()
        {
            Likes++;
        }

        public void React(ReactionType reaction)
        {
            if (Reactions.ContainsKey(reaction)) // == false
            {
                Reactions[reaction]++;
            }
            else 
            { 
                Reactions.Add(reaction, 1); 
            }
        }

        public void AddComment(string content)
        {
            Comment newComment = new Comment(this, content);
            Comments.Add(newComment);
        }
    }

    //TextPost class inheriting from Post class
    public class TextPost : Post
    {
        public int CharacterCount => Content.Length;

        public override string ToString()
        {
            return $"{Content} (Characters: {CharacterCount})";
        }
    }

    //ImagePost class inheriting from Post class
    public class ImagePost : Post
    {
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return $"{Content} (Image: {ImageUrl})";
        }
    }

    //Comment class
    public class Comment
    {
        public string Content { get; set; }
        public DateTime Timestamp {  get; set; } = DateTime.Now;
        public Post ParentPost { get; private set; }

        public Comment(Post parentPost, string content)
        {
            ParentPost = parentPost;
            Content = content;
        }
    }
}
