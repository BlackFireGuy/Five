using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : BasePanel
{
    public GameObject dialog;

    private void Start()
    {
        DialogManager.instance.Dialog = dialog;
    }

}
