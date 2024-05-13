 3D Orchestration Project

**by:**
*Seán Dunne - C21310823*
*Jamie Clarke -*

*Class Group: TU984/3*

**Video:**

[![Submission Video](http://img.youtube.com/vi/qWNQUvIk954/0.jpg)](https://youtu.be/qWNQUvIk954)

# Assignment Brief

For this assignment Jamie and myself (Seán) where tasked with making a 3D sound environment that can be used to experience a concert in VR. Making a modular and changeable environment, for example making it so you can change room types and also change the files that are being used. This environment can potentially be used by creators and listeners.

# Instructions for use

a

# How it works

a

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

a

# What we learned

a