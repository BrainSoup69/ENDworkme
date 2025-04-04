using UnityEngine;

[SerializeField] class Player_AnimationController : MonoBehaviour
{

    [SerializeField] Animator anim;
    enum Emotes { Emote01, Emote02, Emote03 }

    [SerializeField] KeyCode emoteKey_1 = KeyCode.Keypad1;
    [SerializeField] KeyCode emoteKey_2 = KeyCode.Keypad2;
    [SerializeField] KeyCode emoteKey_3 = KeyCode.Keypad3;

    [SerializeField] Rigidbody rb;
    
    void Update()
    {
        if (Input.GetKeyDown(emoteKey_1)) UseEmotes(emoteKey_1);
        if (Input.GetKeyDown(emoteKey_2)) UseEmotes(emoteKey_2);
        if (Input.GetKeyDown(emoteKey_3)) UseEmotes(emoteKey_3);

        float speed = rb.linearVelocity.magnitude;
        anim.SetFloat("speed", speed);
        if(Input.GetButtonUp("Horizontal")|| Input.GetButtonUp("Vertical"))
        {
            int rng = Random.Range(0, 2);
            anim.SetInteger("_IdleState", rng);
        }

    }

    void UpdateEmote(Emotes emote)
    {
        switch (emote)
        {
            case Emotes.Emote01:
                anim.SetInteger("_Emote", (int)Emotes.Emote01);
                break;
            case Emotes.Emote02:
                anim.SetInteger("_Emote", (int)Emotes.Emote02);
                break;
            case Emotes.Emote03:
                anim.SetInteger("_Emote", (int)Emotes.Emote03);
                break;
        }
    }


    void UseEmotes(KeyCode key)
    {

        switch (key)
        {
            case KeyCode.Keypad1:
                UpdateEmote(Emotes.Emote01);
                break;
            case KeyCode.Keypad2:
                UpdateEmote(Emotes.Emote02);
                break;
            case KeyCode.Keypad3:
                UpdateEmote(Emotes.Emote03);
                break;

        }
        anim.SetTrigger("UseEmote");

    }
}
