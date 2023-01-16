using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;

public class Register_LogIn : MonoBehaviour
{
    string username;//Stores username
    string password;//Stores password
    string rePass;//Stores reentered password


    [Header("Information Text")] //Stores the text boxes
    public GameObject textBox;
    public Text isLongEnough;
    public Text hasCaps;
    public Text containsSpecialChar;
    public Text hasNumbers;
    public Text strength;

    [Header("Input Text")] //Takes and stores text input
    public InputField inUser;
    public InputField inPass;
    public InputField reInPass;
    public Button Enter;//Stores button
    public Button Register;

    private int strengthScore; //Stores strength of password

    bool passed; // True if password passes the strength test, false otherwise
    bool isUsername; // True if the password and the username are the same

    private void Start() {
        Register.gameObject.SetActive(false);

        textBox.SetActive(false);

        isLongEnough.color = Color.red;
        hasCaps.color = Color.red;
        containsSpecialChar.color = Color.red;
        hasNumbers.color = Color.red;
    }

    public void getInput(){ //Button will call functions and will store the text in boxes into strings
        username = inUser.text;
        password = inPass.text;
        rePass = reInPass.text;

        checkPasswordEquality();
    }

    void checkPasswordEquality(){ // Will only call the function if both passwords are equal
        if(password == rePass){
            textBox.SetActive(false);
            checkPassStrength();
        }
        else{
            textBox.SetActive(true);
        }
    }

    public int binarySearch(int target, int[] ASCII_ARR, int size){
        int LOW = 0; // Start of the array
        int HIGH = size - 1; // Last integer in array

        int searchCount = 0; // Stores how many times it searches for the target

        while(LOW <= HIGH){
            searchCount++;

            int pivot = (HIGH + LOW) / 2; // Compare this to the target and decide if the number is lower or higher in the array

            if(ASCII_ARR[pivot] == target){
                //Debug.Log(searchCount);
                return 0;
            }

            else if(ASCII_ARR[pivot] < target){
                LOW = pivot + 1; // It will ignore all the numbers lower than the pivot if its smaller than the target integer
            }
            
            else{
                HIGH = pivot - 1; // It will ignore all the numbers higher than the pivot if its smaller than the target integer
            }
        }

        return -1; // If not found
    }

    void checkPassStrength(){
        bool isLong = false;
        bool hasSpecialChar = false;
        bool containsNum = false;
        bool containsCap = false;

        int arrLimit = 0; // Will check how many times it has iterated through the password

        //Array of every special character in ASCII code
        int[] special_in_ASCII = {33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,58,59,60,61,62,63,64,91,92,93,94,95,96,123,124,125,126,127};

        bool[] checks = {isLong, hasSpecialChar, containsNum, containsCap};

        isLongEnough.color = Color.red;
        hasCaps.color = Color.red;
        containsSpecialChar.color = Color.red;
        hasNumbers.color = Color.red;

        strengthScore = 0;

        while(true){
            if(password.Length >= 8){
                isLong = true;

                checks[0] = true;

                isLongEnough.color = Color.green;
            }

            foreach(char c in password){
                
                arrLimit++;

                if(System.Convert.ToInt32(c) >= 65 && System.Convert.ToInt32(c) <= 90){
                    containsCap = true;

                    checks[3] = true;

                    hasCaps.color = Color.green;
                }

                else if(binarySearch(System.Convert.ToInt32(c), special_in_ASCII, special_in_ASCII.Length) == 0){
                    hasSpecialChar = true;

                    checks[2] = true;

                    containsSpecialChar.color = Color.green;
                }

                else if(System.Convert.ToInt32(c) >= 48 && System.Convert.ToInt32(c) <= 57){
                    containsNum = true;

                    checks[1] = true;

                    hasNumbers.color = Color.green;
                }
                
                else{
                    isLongEnough.color = Color.red;
                    hasCaps.color = Color.red;
                    containsSpecialChar.color = Color.red;
                    hasNumbers.color = Color.red;
                }

                if(arrLimit == password.Length){
                    break;
                }
            }

            for(int i = 0; i < checks.Length; i++){
                if(checks[i] == true){
                    strengthScore++;
                    // Debug.Log(checks[i]);
                }
            }

            // Debug.Log(strengthScore);

            while(strengthScore >= 0){
                if(strengthScore > 4){
                    strengthScore = 4;
                }

                break;
            }

            switch (strengthScore){
                case 0:
                    // Debug.Log("Too weak");
                    strength.text = "Password is too weak, write a new one";
                    break;
                case 1:
                    // Debug.Log("Very Weak");
                    strength.text = "Very Weak Password";
                    passed = true;
                    break;
                case 2:
                    // Debug.Log("Okay");
                    passed = true;
                    strength.text = "Weak Password";
                    break;
                case 3:
                    // Debug.Log("Strong");
                    passed = true;
                    strength.text = "Strong Password";
                    break;
                case 4:
                    // Debug.Log("Very Strong");
                    strength.text = "Very Strong Password";
                    passed = true;
                    break;

                default:
                    strength.text = "Not known";
                    break;
            }

            break;
        }

        if(passed){
            Enter.gameObject.SetActive(false);
            Register.gameObject.SetActive(true);
        }else{
            Enter.gameObject.SetActive(true);
            Register.gameObject.SetActive(false);
        }
    }

    void register(){
        hash(password);
    }

    void hash(string hashPass){
        byte[] bytes = Encoding.UTF8.GetBytes(hashPass);

        SHA256 hashAlgo = SHA256.Create();
        byte[] hashed = hashAlgo.ComputeHash(bytes);

        Debug.Log(BitConverter.ToString(hashed).Replace("-", ""));
    }
}