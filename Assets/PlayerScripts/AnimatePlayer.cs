using UnityEngine;
using System.Collections;

public class AnimatePlayer : PlayerControl
{

    private Animator animate;

    // Use this for initialization
    void Start()
    {

        animate = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectingAnimatation()
    {
        switch (state)
        {
            case charState.idle:
                animate.SetFloat("Speed", 0.0f);
                break;

            case charState.moving:
                animate.SetFloat("Speed", 1.0f);
                break;

            case charState.jumping:
                animate.SetBool("Jumping", true);
                break;

            case charState.falling:
                animate.SetBool("Jumping", false);
                break;

            default:
                Debug.Log("Not hitting anything");
                break;
        }

    }


}
