using UnityEngine;
using System.Collections;

public class TextureSwap : MonoBehaviour {

    public Renderer[] renderers;
    //public bool IsSad = false;
    public Texture2D hardTextureGround;
    public Texture2D easyTextureGround;
    public Texture2D hardTextureSky;
    public Texture2D easyTextureSky;
    public Texture2D targetTextureGround;
    public Texture2D targetTextureSky;

    public Animator anim;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (HappinessManager.isHard)
        {
            targetTextureGround = hardTextureGround;
            targetTextureSky = hardTextureSky;
        }
        else
        {
            targetTextureGround = easyTextureGround;
            targetTextureSky = easyTextureSky;
        }
        SwapTexture(targetTextureGround, targetTextureSky);
        anim.SetBool("IsHard", HappinessManager.isHard);
        MusicSwitcher.SongSwitch(HappinessManager.isHard);
	}

    public void SwapTexture(Texture2D textureGround, Texture2D textureSky)
    {
        foreach(var renderer in renderers)
        {
            if (renderer.material.mainTexture == textureGround || renderer.material.mainTexture == textureSky)
                return;
            else
            {
                if(renderer.gameObject.tag == "Sky")
                {
                    renderer.material.mainTexture = textureSky;
                }
                else
                {
                    renderer.material.mainTexture = textureGround;
                }
                
            }
                
        }
    }
}
