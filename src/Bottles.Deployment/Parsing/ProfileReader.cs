using System;
using System.Collections.Generic;
using System.Linq;

namespace Bottles.Deployment.Parsing
{
    public interface IProfileReader
    {
        IEnumerable<HostManifest> Read(string profileDirectory);
    }

    public class ProfileReader : IProfileReader
    {
        private readonly IRecipeSorter _sorter;

        public ProfileReader(IRecipeSorter sorter)
        {
            _sorter = sorter;
        }

        public IEnumerable<HostManifest> Read(string profileDirectory)
        {
            var recipes = RecipeReader.ReadRecipes(profileDirectory);
            recipes = _sorter.Order(recipes);

            // TODO -- harden.  Must be at least 1 recipe
            var firstRecipe = recipes.First();
            recipes.Skip(1).Each(firstRecipe.AppendBehind);

            return firstRecipe.Hosts;
        }
    }

    public interface IRecipeSorter
    {
        IEnumerable<Recipe> Order(IEnumerable<Recipe> recipes);
    }

    public class RecipeSorter : IRecipeSorter
    {
        // TODO -- make this do something real with dependency graphs
        public IEnumerable<Recipe> Order(IEnumerable<Recipe> recipes)
        {
            return recipes;
        }
    }
}