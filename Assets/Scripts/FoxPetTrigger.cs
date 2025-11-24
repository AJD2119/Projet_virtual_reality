using UnityEngine;

public class FoxPetTrigger : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Pet()
    {
        anim.SetTrigger("Pet");
    }

    public void Roulade()
    {
        anim.SetTrigger("roulade");
    }
}
