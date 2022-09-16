namespace Trailblazor.Client.Shared.Helpers
{
    public sealed class ClassBuilder
    {
        /// <summary>
        /// Fires when the class list is modified.
        /// </summary>
        /// <remarks>
        /// If a class name is passed to the <see cref="AddClass(string)"/> or <see cref="RemoveClass(string)"/> methods
        /// and a the class name already exists/doesn't exist, then this event will not fire.
        /// </remarks>
        public event EventHandler? ClassesChanged;

        private readonly List<string> _classes = new();

        /// <summary>
        /// Builds a space-separated string of the classes.
        /// </summary>
        public string Build => string.Join('\u0020', _classes);

        public ClassBuilder()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ClassBuilder"/> and adds the specified classes.
        /// </summary>
        /// <param name="startingClasses"></param>
        public ClassBuilder(params string[] startingClasses)
        {
            _classes.AddRange(startingClasses);
        }

        /// <summary>
        /// Adds the class name to the class list if it does not already exists.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>The <see cref="ClassBuilder"/> instance so that it can be chained.</returns>
        public ClassBuilder AddClass(string className)
        {
            if (!_classes.Contains(className))
            {
                _classes.Add(className);
                ClassesChanged?.Invoke(this, EventArgs.Empty);
            }

            return this;
        }

        /// <summary>
        /// Removes the class name from the class list if it exists.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>The <see cref="ClassBuilder"/> instance so that it can be chained.</returns>
        public ClassBuilder RemoveClass(string className)
        {
            if (_classes.Contains(className))
            {
                _classes.Remove(className);
                ClassesChanged?.Invoke(this, EventArgs.Empty);
            }

            return this;
        }

        /// <summary>
        /// Adds the specified class name if it does not exist in the collection, or removes it if it does.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>The <see cref="ClassBuilder"/> instance so that it can be chained.</returns>
        public ClassBuilder ToggleClass(string className)
        {
            if (_classes.Contains(className))
                _classes.Remove(className);
            else
                _classes.Add(className);

            ClassesChanged?.Invoke(this, EventArgs.Empty);

            return this;
        }

        /// <summary>
        /// Does a case-sensitive check if the class list contains the class name.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>
        /// true if the class list contains the class name, otherwise, returns false.
        /// </returns>
        public bool HasClass(string className)
        {
            return _classes.Contains(className);
        }
    }
}
