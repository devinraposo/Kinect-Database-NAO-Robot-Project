using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class SaveDataScript : MonoBehaviour {

    InstrumentScript instrScript;
    BodySourceView bodyView;
    void Awake()
    {
        instrScript = GameObject.Find("Instrument").GetComponent<InstrumentScript>();
        instrScript.gameObject.SetActive(false);
        bodyView = GameObject.Find("BodyView").GetComponent<BodySourceView>();
    }
    public void SaveNAOData()
    {
        using (StreamWriter writeText = new StreamWriter("nao.txt"))
        {
            for(int i = 0; i < instrScript.timeList.Count; ++i)
            {
                writeText.Write(instrScript.boxList[i] + " " + instrScript.timeList[i] + "\n");
            }
        }
    }
    public void SaveGestureData()
    {
        using (StreamWriter writeText = new StreamWriter("gesture.txt"))
        {
            for(int i = 0; i < bodyView.vectorList[bodyView._body.transform.childCount-1].Count; ++i)
            {
                for(int j = 0; j < bodyView._body.transform.childCount; ++j)
                {
                    writeText.Write(j + " " + bodyView.vectorList[j][i].x + " " + bodyView.vectorList[j][i].y +
                        " " + bodyView.vectorList[j][i].z + "\n");
                }
            }
        }
    }

    //Below is the code from the following URL ported to C# by Devin Raposo:
    //http://kevinboone.net/javamidi.html
    // Note lengths
    //  We are working with 32 ticks to the crotchet. So
    //  all the other note lengths can be derived from this
    //  basic figure. Note that the longest note we can
    //  represent with this code is one tick short of a 
    //  two semibreves (i.e., 8 crotchets)
    static int SEMIQUAVER = 4;
    static int QUAVER = 8;
    static int CROTCHET = 16;
    static int MINIM = 32;
    static int SEMIBREVE = 64;
    // Standard MIDI file header, for one-track file
    // 4D, 54... are just magic numbers to identify the
    //  headers
    // Note that because we're only writing one track, we
    //  can for simplicity combine the file and track headers
    int[] header = new int[]
       {
     0x4d, 0x54, 0x68, 0x64, 0x00, 0x00, 0x00, 0x06,
     0x00, 0x00, // single-track format
     0x00, 0x01, // one track
     0x00, 0x10, // 16 ticks per quarter
     0x4d, 0x54, 0x72, 0x6B
       };
    // Standard footer
    int[] footer = new int[] { 0x01, 0xFF, 0x2F, 0x00 };
    // A MIDI event to set the tempo
    int[] tempoEvent = new int[]
    {
         0x00, 0xFF, 0x51, 0x03,
         0x0F, 0x42, 0x40 // Default 1 million usec per crotchet
    };
    // A MIDI event to set the key signature. This is irrelevent to
    //  playback, but necessary for editing applications 
    int[] keySigEvent = new int[]
    {
         0x00, 0xFF, 0x59, 0x02,
         0x00, // C
         0x00  // major
    };
    // A MIDI event to set the time signature. This is irrelevent to
    //  playback, but necessary for editing applications 
    int[] timeSigEvent = new int[]
    {
         0x00, 0xFF, 0x58, 0x04,
         0x04, // numerator
         0x02, // denominator (2==4, because it's a power of 2)
         0x30, // ticks per click (not used)
         0x08  // 32nd notes per crotchet 
    };
    // The collection of events to play, in time order
    protected List<int[]> playEvents;
    /** Write the stored MIDI events to a file */
    void writeToFile(string filename)
    {
        FileStream fos = new FileStream(filename, FileMode.OpenOrCreate);

        for (int i = 0; i < header.Length; ++i)
        {
            fos.WriteByte((byte)header[i]);
        }

        // Calculate the amount of track data
        // _Do_ include the footer but _do not_ include the 
        // track header

        int size = tempoEvent.Length + keySigEvent.Length + timeSigEvent.Length
            + footer.Length;

        for (int i = 0; i < playEvents.Count; i++)
            size += playEvents[i].Length;

        // Write out the track data size in big-endian format
        // Note that this math is only valid for up to 64k of data
        //  (but that's a lot of notes) 
        int high = size / 256;
        int low = size - (high * 256);
        fos.WriteByte((byte)0);
        fos.WriteByte((byte)0);
        fos.WriteByte((byte)high);
        fos.WriteByte((byte)low);


        // Write the standard metadata — tempo, etc
        // At present, tempo is stuck at crotchet=60 
        for (int i = 0; i < tempoEvent.Length; ++i)
            fos.WriteByte((byte)tempoEvent[i]);
        for (int i = 0; i < keySigEvent.Length; ++i)
            fos.WriteByte((byte)keySigEvent[i]);
        for (int i = 0; i < timeSigEvent.Length; ++i)
            fos.WriteByte((byte)timeSigEvent[i]);

        // Write out the note, etc., events
        for (int i = 0; i < playEvents.Count; i++)
        {
            for (int j = 0; j < playEvents[i].Length; ++j)
                fos.WriteByte((byte)playEvents[i][j]);
        }

        // Write the footer and close
        for (int i = 0; i < footer.Length; ++i)
            fos.WriteByte((byte)footer[i]);
        fos.Close();
    }
    /** Store a note-on event */
    void noteOn(int delta, int note, int velocity)
    {
        int[] data = new int[4];
        data[0] = delta;
        data[1] = 0x90;
        data[2] = note;
        data[3] = velocity;
        playEvents.Add(data);
    }
    /** Store a note-off event */
    void noteOff(int delta, int note)
    {
        int[] data = new int[4];
        data[0] = delta;
        data[1] = 0x80;
        data[2] = note;
        data[3] = 0;
        playEvents.Add(data);
    }
    /** Test method — creates a file test1.mid when the class
        is executed */
    public void SaveMIDIData()
    {
        playEvents = new List<int[]>();
        ulong offset = 0;
        for (int i = 0; i < instrScript.timeList.Count; ++i)
        {
            noteOn((int)offset, instrScript.boxList[i]+52, 127);
            noteOff((int)offset+10, instrScript.boxList[i]+52);
            offset += (instrScript.timeList[i]);
        }
        writeToFile("test1.mid");
    }
}