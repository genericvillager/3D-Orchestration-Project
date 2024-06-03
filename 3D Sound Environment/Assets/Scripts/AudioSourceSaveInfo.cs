
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSaveInfo {
    public string Name { get; set; }
    public string Position { get; set; }
    public string Rotation { get; set; }
    public string File { get; set; }
    public float Vol {get; set;}
    public float Pitch {get; set;}
    public float StereoPan {get; set;}
    public float SpatialBlend {get; set;}
    public float ReverbZoneMix {get; set;}
    public float DopplerLevel {get; set;}
    public float Spread {get; set;}
    public float DryLevel {get; set;}
    public float Room {get; set;}
    public float RoomHF {get; set;}
    public float RoomLF {get; set;}
    public float DecayTime {get; set;}
    public float DecayHFRatio {get; set;}
    public float ReflectionsLevel {get; set;}
    public float ReverbLevel {get; set;}
    public float ReverbDelay {get; set;}
    public float ReflectionsDelay {get; set;}
    public float HfReference {get; set;}
    public float LfReference {get; set;}
    public float Diffusion {get; set;}
    public float Density {get; set;}
    
    public AudioSourceSaveInfo(
        string name, 
        Vector3 position, 
        Vector3 rotation, 
        string file, 
        float vol,
        float pitch,
        float stereoPan,
        float spatialBlend,
        float reverbZoneMix,
        float dopplerLevel,
        float spread,
        float dryLevel,
        float room,
        float roomHF,
        float roomLF,
        float decayTime,
        float decayHFRatio,
        float reflectionsLevel,
        float reverbLevel,
        float reverbDelay,
        float reflectionsDelay,
        float hfReference,
        float lfReference,
        float diffusion,
        float density) 
    {
        Name = name;
        Position = position.ToString();
        Rotation = rotation.ToString();
        File = file;

        //paramaters
        Vol = vol;
        Pitch = pitch;
        StereoPan = stereoPan;
        SpatialBlend = spatialBlend;
        ReverbZoneMix = reverbZoneMix;
        DopplerLevel = dopplerLevel;
        Spread = spread;
        DryLevel = dryLevel;
        Room = room;
        RoomHF = roomHF;
        RoomLF = roomLF;
        DecayTime = decayTime;
        DecayHFRatio = decayHFRatio;
        ReflectionsLevel = reflectionsLevel;
        ReverbLevel = reverbLevel;
        ReverbDelay = reverbDelay;
        ReflectionsDelay = reflectionsDelay;
        HfReference = hfReference;
        LfReference = lfReference;
        Diffusion = diffusion;
        Density = density;
    }
}