using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayCommand : ICommand
{
    private AudioSource _source;

    public SoundPlayCommand(AudioSource source)
    {
        _source = source;
    }

    public void Execute()
    {
        _source.Play();
    }
}
