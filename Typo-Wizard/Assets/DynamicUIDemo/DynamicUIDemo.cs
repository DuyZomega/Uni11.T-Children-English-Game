using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @kurtdekker

public class DynamicUIDemo : MonoBehaviour
{
	public OneSingleTile ExemplarTile;

	OneSingleTile MakeFreshCopyOfExampleTile()
	{
		// create it and simultaneously parent it to the same place in the UI
		var copy = Instantiate<OneSingleTile>( ExemplarTile, ExemplarTile.transform.parent);

		// make it visible
		copy.gameObject.SetActive( true);

		return copy;
	}

	void Start ()
	{
		// turn off the example first, so it doesn't interfere
		ExemplarTile.gameObject.SetActive( false);

		// you choose how to create these, perhaps from a list
		// of known levels, or a list of items, for instance.

		// This code searches Resources/DynamicUISprites and loads
		// all the sprites it finds, sorts them by alphabetical
		// name, and displays them in the grid.

		var sprites = Resources.LoadAll<Sprite>( "DynamicUISprites/");

		Debug.Log( System.String.Format( "I found {0} sprites.", sprites.Length));

		// alpha sort by name
		System.Array.Sort( sprites, (a,b) => { return a.name.CompareTo( b.name); });

		// now make buttons out of each one
		foreach( var sprite in sprites)
		{
			var entry = MakeFreshCopyOfExampleTile();

			entry.SetIconSprite( sprite);

			entry.SetCaptionText( sprite.name);

			// set up what each button does when pressed
			{
				string textToDisplay = "You clicked on:" + sprite.name;

				entry.SetButtonDelegate(
					() => 
					{
						Debug.Log( textToDisplay);

						// TODO you can call whatever other code you like here...
					}
				);
			}
		}
	}
}
