﻿using System.ComponentModel.DataAnnotations;


namespace Bachelor_Server.Models.Authorization
{
    public class OAuth2Model
    {
        [Key] public int Id { get; set; }
        public string AccessToken { get; set; }
        public string HeaderPrefix { get; set; }
    }
}