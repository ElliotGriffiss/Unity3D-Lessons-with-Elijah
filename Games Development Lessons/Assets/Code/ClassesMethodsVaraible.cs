using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassesMethodsVaraible : MonoBehaviour
{
    // int : An integer(eg, 3).
    // Can be a whole number between -2147483648 and 2147483647
    int IntVariable;


    // float : A fractional(floating point) number(eg, 3.25907).
    // Can be a number between roughly 1.5 x 10^45 to 3.4 10^38, in floating point format.
    float FloatVariable;


    // String : A sequence of characters(eg, "Hello User 6555")
    // (no specified maximum length, as far as I'm aware!)
    string StringVariable;


    // boolean : A true/false value.
    // Can only contain either the value true or false.
    bool BoolVariable;


    //Public and Private Variables
    public int PublicIntVariable; // public int variable, this can be access inside and outside of this class.
    private int PrivateIntVariable; // private int variable, this can be access inside of this class.


    // Methods / Functions

    /// <summary>
    /// A private method that can be inside of this class
    /// </summary>
    private void EmptyPrivateMethod()
    {

    }

    /// <summary>
    /// A public method that can be access inside and outside of this class.
    /// </summary>
    public void EmptyPublicMethod()
    {

    }

    /// <summary>
    /// This function returns an interger value.
    /// </summary>
    /// <returns></returns>
    public int ReturnInValue()
    {
        int intVariable = 0; // this int variable is decleared everytime this function is called.
        return intVariable;
    }

    /// <summary>
    /// This function requires 2 int values and returns them sum of them.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int AddInt(int a, int b)
    {
        return a + b;
    }
}
