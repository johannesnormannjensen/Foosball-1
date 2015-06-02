using System;
using System.ComponentModel.DataAnnotations;

namespace Foosball.Models.FoosballModels
{
//    Created by Ferenc

    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}