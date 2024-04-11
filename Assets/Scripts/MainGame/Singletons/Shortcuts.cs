namespace MainGame.Singletons
{
	public static class Shortcuts
	{
		/// <summary>
		/// Get singleton
		/// </summary>
		/// <typeparam name="T">Singleton type</typeparam>
		/// <returns>Singleton instance</returns>
		public static T Get<T>() where T : class
		{
			return Singleton<T>.Instance;
		}
		/// <summary>
		/// Init singleton with the instance
		/// </summary>
		/// <param name="instance">Instance to set as singleton</param>
		/// <typeparam name="T">Type of singleton</typeparam>
		public static void InitSingleton<T>(T instance) where T : class
		{
			Singleton<T>.Initialise(instance);
		}
		/// <summary>
		/// Check is singleton is set
		/// </summary>
		/// <typeparam name="T">Type of singleton</typeparam>
		/// <returns>True if singleton exists, false otherwise</returns>
		public static bool IsSingletonSet<T>() where T : class
		{
			return Singleton<T>.Exists;
		}

	}
}
