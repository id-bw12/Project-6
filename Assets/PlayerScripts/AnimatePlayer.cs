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

    public void SelectingAnimatation(float horInput, float vertInput)
    {


        switch (state)
        {
            case charState.idle:
                animate.SetFloat("Speed", 0.0f);
                break;

            case charState.moving:
                if (horInput != 0.0f)
                {
                    animate.SetFloat("Speed", 2.0f);

                    //if moving to the left 
                    if (horInput < 0.0f)
                        animate.SetFloat("Forward", 0.4f);
                    else //If moving to the right
                        if (horInput > 0.0f)
                        animate.SetFloat("Forward", 0.9f);

                }
                else //if not moving to the right or left
            if (horInput == 0 && vertInput != 0)
                {
                    animate.SetFloat("Speed", 1.0f);
                    animate.SetFloat("Forward", 0.0f);
                }
                break;

            case charState.jumping:
                animate.SetBool("Jumping", true);

                if (vertInput != 0 && horInput == 0)
                {
                    animate.SetFloat("Speed", 2.0f);
                    animate.SetFloat("Forward", 0.9f);
                }
                else
                    if (horInput != 0)
                {

                    if (horInput < 0.0f)
                        animate.SetFloat("Forward", 0.4f);
                    else //If moving to the right
                        if (horInput > 0.0f)
                        animate.SetFloat("Forward", 0.6f);
                }

                break;

            case charState.falling:
                break;

            default:
                Debug.Log("Not hitting anything");
                break;
        }

    }


}
