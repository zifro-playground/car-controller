using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for level settings. You must implement the method setGameFunctions().
/// </summary>
public abstract class LevelSettings {

	/// Sets the functions that the user should be able to use.
	protected abstract void setGameFunctions();

	/// Set the pre code that is not editable for the user.
	protected abstract void setPreCode();

	/// Set the code that should be given to the user at start.
	protected abstract void setCode();
}
