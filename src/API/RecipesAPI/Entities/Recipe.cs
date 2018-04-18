using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Entities
{
    [DataContract]
    public class Recipe
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RecipeText { get; set; }

        [DataMember]
        public string ImagePath { get; set; }
    }
}
