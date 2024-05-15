# 3D Orchestration Project

**by:**

*Seán Dunne - C21310823*

*Jamie Clarke - C21364153*

*Class Group: TU984/3*

**Video:**

[![Submission Video](https://drive.google.com/uc?export=view&id=13_yBFS2up6_f5GjiGcvf7JHOdbvVqdiF)](https://www.youtube.com/watch?v=EF6ugf2otz8)

# Assignment Brief

For this assignment Jamie and myself (Seán) where tasked with making a 3D sound environment that can be used to experience a concert in VR. Making a modular and changeable environment, for example making it so you can change room types and also change the files that are being used. This environment can potentially be used by creators and listeners.

# Instructions for use

* Download this project from our Itch.
* You can get a link for the game here - [3D Orchestration Project](https://jamie-clarke.itch.io/3d-sound-environment).
* Once you have downloaded the build, unzip the files into an area that would suit you the user.
* Run the game exe file which is called 3D Orchestration Project.
* When you download the project there will be sample sound files already available for you to test out our programme with.
* Fot sound files you would like to bring in please put it into the relevant sound file folder "3D Sound Environment_Data/Resources/AudioFiles". When you are importing a file into the game you will have to use the import button on the audio source click on the file you wish to use and then hit the import button again to close the UI.
* Then press on.
* Finally play from start and enjoy!!

# How it works

User Perspective:
1. You start up the program
2. Add new Audio Source
3. Select Audio Clip from Resources Folder in Game Folder
4. Move Audio Source to desired location
5. repeat steps 2 - 4 until content
6. press on
7. play from start and Enjoy!

optionally you can change the room type, make the audio sources go Solo / Mute them individually, move around, or add crowd members to change how the audio sounds when they get in your way.

In the Background:

The audio Sources all have a raycast that shoots towards the player, if it hits anything then it will change its audio properties to reflect whatever the object it hits is. you can also change them while playing and move them around live in order to fine-tune everything live.
Surfaces have a tag that the audio source recognises and is able to recognise and react accordingly.
The Desk in the middle can turn the sources on and off, start all sources from the beginning, mute all sources, change the environment, add new audio sources, crowd members and eventually a TV.

The audio sources clips all have to be the same length and sample rate, this is for syncing reasons, they are placed in the *"3D Sound Environment_Data/Resources/AudioFiles"* folder of the Game, these files will become selectable when the change file button is pressed on the audio source.

# List of classes and assets in the project

| Classes | Source |
|-----------|-----------|
| Audio Manager | Jamie Clarke |
| AudioSourceController  | Jamie Clarke |
| AudioSourceName | Seán Dunne |
| AudioSpectrum | Jamie Clarke |
| Room Manager  | Jamie Clarke |
| File Manager  | Jamie Clarke |
| LoadFileToAudioSource  | Jamie Clarke |

**Sample from AudioSourceController.cs**
``` C#
    private void HitDetect()
    {
        RaycastHit hit;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, AS.maxDistance))
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            switch (hit.transform.tag)
            {
                case "Player":
                    ChangeAudio();
                    break;

                case "Crowd":
                    ChangeAudio(vol: .9f, reverbDelay: .07f);
                    break;

                case "Wall":
                    ChangeAudio(vol: .5f, reverbDelay: .09f, diffusion: 50, decayHFRatio: 1);
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.yellow);
        }
        
    }

    public void SwitchAudioFile(AudioClip clip, string fileName)
    {
        print(clip.name);
        AS.Stop();
        AS.clip = clip;
        if (isPlaying)
        {
            AS.Play();
            SynchroniseWithMain();
        }
        audioName.text = fileName;
    }
```

| Asset| Source |
|-----------|-----------|
| Crowd  | Jamie Clarke  |
| Audio Source  | Jamie Clarke  |
| Control Panel | Seán Dunne |
| Canvas Holder | Seán Dunne |
| Ground | Jamie Clarke |
| Audio Desk | Jamie Clarke  |
| Wall  | Jamie Clarke  |
| VR Controller | Meta XR All-in-One SDK  |
| Hands | Meta XR All-in-One SDK |
| Teleport Points | Meta XR All-in-One SDK |


Audio Source:

![AudioSource](https://imgur.com/87o76yT.png)

# References

* [Meta XR All-in-One SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)
* [Unity Reference](https://docs.unity3d.com/ScriptReference/)

# What we are most proud of in the assignment

I (Seán) am most proud of the feel of this project in VR. It is comfortable to use, doesn't cause motion sickness and is also intuitive to use for people who may have never used VR before. There is also the feeling of the sound when you move around in VR. There is a huge amount of impact when listening to the audio and hear it changing as you move your head around.

I (Jamie) am most proud of the Audio system I developed in the Engine, although the current build doesn't use its full potential it can dynamically change to its environment, something you can see if you spawn in a crowd member between you and a sound source. it had many bugs and performance issues, but now I can easily add different materials and audio properties to change how the listener hears the sound.


# What we learned

I (Seán) learned how to use and work with the basic features Meta XR All-in-One SDK. I also learned how to implement those features in a way that suited the project I was working on, for example gave the audio sources multiple options in how they can be picked up for ease of use. Messing around and changing implementation of the current Meta XR components allowed this project to have an intuitive feeling for people who have never used VR. Using this package provided by meta has also allowed us to integrate hand tracking into the project so that both controllers or hand tracking can be used.

I (Jamie) learned how to make and build games for VR, it is very different than normal non-VR-games, especially when considering how the player will interact with things. I had to make sure that the main desk and the buttons on the audio sources worked intuitively, and didn't cause more hassle than they needed to. as well another thing I learned is that in VR the body of the player works differently than a capsule, so I needed to alter the code for the audioSources to counter this issue as the ray cast misses the player often

