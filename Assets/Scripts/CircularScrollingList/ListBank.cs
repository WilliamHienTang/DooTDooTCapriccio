/* Store the contents for ListBoxes to display.
 */
using UnityEngine;

public class ListBank : MonoBehaviour
{
	public static ListBank Instance;

	public string[] contents;

	void Awake()
	{
		Instance = this;
	}

	public string getListContent(int index)
	{
		return contents[index].ToString();
	}

	public int getListLength()
	{
		return contents.Length;
	}
}
