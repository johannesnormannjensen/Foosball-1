using System;
using System.ComponentModel.DataAnnotations;

namespace Foosball.Models.FoosballModels
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}