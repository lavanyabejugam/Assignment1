using System;

namespace Add2BinaryFloats
{
    class AddTwoBinary
    {
        public string Perform2sCompliment(string str){
        char[] arr = str.ToCharArray();
        int idx;
        for (idx = arr.Length-1; idx >= 0 ; idx--){ 
            if (str[idx] == '1') 
                break; 
        }
        for (int k = idx - 1 ; k >= 0; k--) { 
            if (arr[k] == '1'){ 
                arr[k] = '0'; 
            }
            else if(str[k] == '.')
                continue;
            else
                arr[k] = '1'; 
        }
        string Str = new string(arr);
        return Str; 
    }
    
    public string AddBinary(string Str1, string Str2, float FirstNum, float SecondNum) 
    { 
        string result = "";
        int MaxLength = Str1.Length > Str2.Length ? Str1.Length : Str2.Length;
        int Carry = 0, Sum;
        
        /* Adding two binary strings*/
        for(int k = MaxLength - 1; k >= 0 ; k--)
        {
            if(Str1[k] == '.'){
                result = '.' + result;
                continue;
            }
            Sum = ((Str1[k] + Str2[k] + Carry) - 96) % 2;
            Carry = ((Str1[k] + Str2[k] + Carry) - 96) /2;
            result = Sum + result;
        }
        
        Console.WriteLine(result);
        Console.WriteLine(Carry);
        
        /* If both the Inputs are positive */ 
        if(Carry == 1 && FirstNum > 0 && SecondNum > 0)
            result = Carry + result;
        
        /* If both the Inputs are negative */    
        else if(Carry == 0 && FirstNum < 0 && SecondNum < 0){
            result = Perform2sCompliment(result);
            result = "1" + result;
        }
        
        else if(Carry == 1 && FirstNum < 0 && SecondNum < 0)
            result = Perform2sCompliment(result);
        
        else if(((FirstNum < 0) && Math.Abs(FirstNum) > SecondNum) || ((SecondNum < 0) && Math.Abs(SecondNum) > FirstNum)){
            result = Perform2sCompliment(result);
        }
        
        return result; 
    }
    
    public string FloatToBinary(float FloatNum, int Precision)
    {
        int Integer, Remainder; 
        string BinaryInteger = "", BinaryDecimal = "";
        Integer = (int)FloatNum;
        float Fraction = FloatNum - Integer;
        
        /* Convert Integeral part of input into binary format */  
        while(Integer != 0)
        {
            Remainder = Integer % 2;
            BinaryInteger = Remainder + BinaryInteger;
            Integer = Integer/2;
        } 
        
        /* Convert decimal part of input into binary format */ 
        while (Precision != 0) 
        { 
        BinaryDecimal = BinaryDecimal + (int)(Fraction * 2); 
        Fraction = (Fraction * 2) - (int)(Fraction * 2);
        Precision--;
        } 
        
        return BinaryInteger + '.' + BinaryDecimal;
    }
	public static void Main()
	{
	    /* Create an object for the class */
	    AddTwoBinary obj = new AddTwoBinary();
	    
		/* Take two Floating Inputs */
		float FirstNum, SecondNum;
		Console.WriteLine("Enter the first float value : ");
		FirstNum = (float)Convert.ToDouble(Console.ReadLine());
		
        Console.WriteLine("Enter the first float value : ");
		SecondNum = (float)Convert.ToDouble(Console.ReadLine());
		
		/* Convert Floating inputs to binary strings */
		string binaryf1, binaryf2;
        binaryf1 = obj.FloatToBinary(Math.Abs(FirstNum), 8);
        binaryf2 = obj.FloatToBinary(Math.Abs(SecondNum), 8);
        
        Console.WriteLine("Binary Representation of FirstNum : "+binaryf1);
        Console.WriteLine("Binary Representation of SecondNum : "+binaryf2);
        
        /* Make lengths of two binary strings equall */
        int l1 = binaryf1.Length;
        int l2 = binaryf2.Length;
        if(binaryf1.Length > binaryf2.Length){
            for(int i1 = 0; i1 < (l1-l2); i1++){
                binaryf2 = '0' + binaryf2;
            }
        }
        else if(binaryf1.Length < binaryf2.Length) {
            for(int i2 = 0; i2 < (l2-l1); i2++){
                binaryf1 = '0' + binaryf1;
            }
        }
        
        Console.WriteLine("Binary Representation of FirstNum after making lengths equall : "+binaryf1);
        Console.WriteLine("Binary Representation of SecondNum after making lengths equall : "+binaryf2);
        
        /* perform 2's compliment incase of negative floating input */
        if(FirstNum < 0){
            binaryf1 = obj.Perform2sCompliment(binaryf1);
        }
        if(SecondNum < 0){
            binaryf2 = obj.Perform2sCompliment(binaryf2);
        }
        
        Console.WriteLine("after"+binaryf1);
        Console.WriteLine("after"+binaryf2);
        
        /* Add two binary strings */
        string result = obj.AddBinary(binaryf1, binaryf2, FirstNum, SecondNum);
        Console.WriteLine(result);
        
        /* convert two result into float */
        int dot = result.IndexOf(@".");
        double IntegerPartSum = 0, p = 0;
        for(int i = dot-1; i >= 0; i--){
            IntegerPartSum = (Char.GetNumericValue(result[i])* Math.Pow(2, p)) + IntegerPartSum;
            p++;
        }
        
        double DecimalPartSum = 0;
        p = -1;
        for(int j = dot+1; j < result.Length; j++){
            DecimalPartSum = ((Char.GetNumericValue(result[j])) * (Math.Pow(2, p))) + DecimalPartSum;
            p--;
        }
        
        if(((FirstNum < 0) && Math.Abs(FirstNum) > SecondNum) || ((SecondNum < 0) && Math.Abs(SecondNum) > FirstNum)){
            Console.WriteLine("Float value of corresponding binaryformat : ");
            Console.WriteLine("-{0}",IntegerPartSum + DecimalPartSum);
            Console.ReadLine();
        }
        else{
            Console.WriteLine("Float value of corresponding binaryformat : ");
            Console.WriteLine(IntegerPartSum + DecimalPartSum);
            Console.ReadLine();
        }
    }    
    }
}