﻿namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
    }
}
