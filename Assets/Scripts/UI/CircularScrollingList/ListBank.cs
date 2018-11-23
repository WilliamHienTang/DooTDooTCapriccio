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
        CircularReorder();
    }

    // Reorder contents so that the selected song is the first element
    void CircularReorder()
    {
        string song = PlayerPrefs.GetString(Constants.selectedSongTitle);
        if (song == null)
        {
            return;
        }

        int songIndex = 0;
        for(int i = 0; i < contents.Length; i++)
        {
              if (contents[i] == song)
              {
                    songIndex = i;
              }
        }

        string[] temp = (string[]) contents.Clone();

        for (int i = songIndex; i < contents.Length; i++)
        {
            contents[i - songIndex] = temp[i];
        }

        for (int i = 0; i < songIndex; i++)
        {
            contents[contents.Length - songIndex + i] = temp[i]; 
        }
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
