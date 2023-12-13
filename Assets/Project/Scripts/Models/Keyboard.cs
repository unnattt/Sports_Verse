using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Keyboard : MonoBehaviour
{
/*    [SerializeField] Button one_Key;
    [SerializeField] Button two_Key;
    [SerializeField] Button three_Key;
    [SerializeField] Button four_Key;
    [SerializeField] Button five_Key;
    [SerializeField] Button six_Key;
    [SerializeField] Button seven_Key;
    [SerializeField] Button eight_Key;
    [SerializeField] Button nine_Key;
    [SerializeField] Button zero_Key;*/
    [SerializeField] Button q_Key;
    [SerializeField] Button w_Key;
    [SerializeField] Button e_key;
    [SerializeField] Button r_Key;
    [SerializeField] Button t_Key;
    [SerializeField] Button y_Key;
    [SerializeField] Button u_Key;
    [SerializeField] Button i_Key;
    [SerializeField] Button o_Key;
    [SerializeField] Button a_Key;
    [SerializeField] Button s_Key;
    [SerializeField] Button d_Key;
    [SerializeField] Button f_Key;
    [SerializeField] Button g_Key;
    [SerializeField] Button h_Key;
    [SerializeField] Button j_Key;
    [SerializeField] Button k_Key;
    [SerializeField] Button l_Key;
    [SerializeField] Button z_Key;
    [SerializeField] Button x_Key;
    [SerializeField] Button c_Key;
    [SerializeField] Button v_Key;
    [SerializeField] Button b_Key;
    [SerializeField] Button n_Key;
    [SerializeField] Button m_Key;
    [SerializeField] Button p_Key;
    // [SerializeField] Button semicolonKey;
    // [SerializeField] Button singleQuoteKey;
    // [SerializeField] Button squareOpenBracketKey;
    // [SerializeField] Button squareCloseBracketKey;
    // [SerializeField] Button minusKey;
    // [SerializeField] Button equalKey;
    // [SerializeField] Button forwardSlashKey;
    // [SerializeField] Button commaKey;
    // [SerializeField] Button fullstopKey;
    // [SerializeField] Button backwardSlashKey;
    // [SerializeField] Button rShiftKey;
    // [SerializeField] Button lShiftKey;
    // [SerializeField] Button enterKey;
    // [SerializeField] Button tabKey;
    // [SerializeField] Button capsKey;
    // [SerializeField] Button hashtagKey;
    [SerializeField] Button backspace_Key;
    [SerializeField] Button space_Key;

    [SerializeField] TMP_InputField nameInput;

    private void Awake()
    {
/*        one_Key.onClick.AddListener(One_KeyPressed);
        two_Key.onClick.AddListener(Two_KeyPressed);
        three_Key.onClick.AddListener(Three_KeyPressed);
        four_Key.onClick.AddListener(Four_KeyPressed);
        five_Key.onClick.AddListener(Five_KeyPressed);
        six_Key.onClick.AddListener(Six_KeyPressed);
        seven_Key.onClick.AddListener(Seven_KeyPressed);
        eight_Key.onClick.AddListener(Eight_KeyPressed);
        nine_Key.onClick.AddListener(Nine_KeyPressed);
        zero_Key.onClick.AddListener(Zero_KeyPressed);*/

        q_Key.onClick.AddListener(Q_KeyPressed);
        w_Key.onClick.AddListener(W_KeyPressed);
        e_key.onClick.AddListener(E_KeyPressed);
        r_Key.onClick.AddListener(R_KeyPressed);
        t_Key.onClick.AddListener(T_KeyPressed);
        y_Key.onClick.AddListener(Y_KeyPressed);
        u_Key.onClick.AddListener(U_KeyPressed);
        i_Key.onClick.AddListener(I_KeyPressed);
        o_Key.onClick.AddListener(O_KeyPressed);
        p_Key.onClick.AddListener(P_KeyPressed);
        a_Key.onClick.AddListener(A_KeyPressed);
        s_Key.onClick.AddListener(S_KeyPressed);
        d_Key.onClick.AddListener(D_KeyPressed);
        f_Key.onClick.AddListener(F_KeyPressed);
        g_Key.onClick.AddListener(G_KeyPressed);
        h_Key.onClick.AddListener(H_KeyPressed);
        j_Key.onClick.AddListener(J_KeyPressed);
        k_Key.onClick.AddListener(K_KeyPressed);
        l_Key.onClick.AddListener(L_KeyPressed);
        z_Key.onClick.AddListener(Z_KeyPressed);
        x_Key.onClick.AddListener(X_KeyPressed);
        c_Key.onClick.AddListener(C_KeyPressed);
        v_Key.onClick.AddListener(V_KeyPressed);
        b_Key.onClick.AddListener(B_KeyPressed);
        n_Key.onClick.AddListener(N_KeyPressed);
        m_Key.onClick.AddListener(M_KeyPressed);

        space_Key.onClick.AddListener(space_KeyPressed);
        backspace_Key.onClick.AddListener(Backspace_KeyPressed);
    }

    public void One_KeyPressed()
    {
        nameInput.text += "1";
    }
    public void Two_KeyPressed()
    {
        nameInput.text += "2";
    }
    public void Three_KeyPressed()
    {
        nameInput.text += "3";
    }
    public void Four_KeyPressed()
    {
        nameInput.text += "4";
    }
    public void Five_KeyPressed()
    {
        nameInput.text += "5";
    }
    public void Six_KeyPressed()
    {
        nameInput.text += "6";
    }
    public void Seven_KeyPressed()
    {
        nameInput.text += "7";
    }
    public void Eight_KeyPressed()
    {
        nameInput.text += "8";
    }
    public void Nine_KeyPressed()
    {
        nameInput.text += "9";
    }
    public void Zero_KeyPressed()
    {
        nameInput.text += "0";
    }
    public void Q_KeyPressed()
    {
        nameInput.text += "q";
    }
    public void W_KeyPressed()
    {
        nameInput.text += "w";
    }
    public void E_KeyPressed()
    {
        nameInput.text += "e";
    }
    public void R_KeyPressed()
    {
        nameInput.text += "r";
    }
    public void T_KeyPressed()
    {
        nameInput.text += "t";
    }
    public void Y_KeyPressed()
    {
        nameInput.text += "y";
    }
    public void P_KeyPressed()
    {
        nameInput.text += "p";
    }
    public void O_KeyPressed()
    {
        nameInput.text += "o";
    }
    public void U_KeyPressed()
    {
        nameInput.text += "u";
    }
    public void I_KeyPressed()
    {
        nameInput.text += "i";
    }
    public void A_KeyPressed()
    {
        nameInput.text += "a";
    }
    public void S_KeyPressed()
    {
        nameInput.text += "s";
    }
    public void D_KeyPressed()
    {
        nameInput.text += "d";
    }
    public void F_KeyPressed()
    {
        nameInput.text += "f";
    }
    public void G_KeyPressed()
    {
        nameInput.text += "g";
    }
    public void H_KeyPressed()
    {
        nameInput.text += "h";
    }
    public void J_KeyPressed()
    {
        nameInput.text += "j";
    }
    public void K_KeyPressed()
    {
        nameInput.text += "k";
    }
    public void L_KeyPressed()
    {
        nameInput.text += "l";
    }
    public void Z_KeyPressed()
    {
        nameInput.text += "z";
    }
    public void X_KeyPressed()
    {
        nameInput.text += "x";
    }
    public void C_KeyPressed()
    {
        nameInput.text += "c";
    }
    public void V_KeyPressed()
    {
        nameInput.text += "v";
    }
    public void B_KeyPressed()
    {
        nameInput.text += "b";
    }
    public void N_KeyPressed()
    {
        nameInput.text += "n";
    }
    public void M_KeyPressed()
    {
        nameInput.text += "m";
    }
    public void space_KeyPressed()
    {
        nameInput.text += " ";
    }
    public void Backspace_KeyPressed()
    {
        Debug.Log($"Lenght: {nameInput.text.Length}, Index: {nameInput.text.Length - 1}");
        if (nameInput.text.Length < 0)
        {
            return;
        }
        nameInput.text = nameInput.text.Remove(nameInput.text.Length - 1, 1);
    }
}
