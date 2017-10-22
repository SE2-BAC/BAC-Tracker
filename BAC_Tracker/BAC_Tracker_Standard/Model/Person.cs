using System;

public class Person
{
    public Person exampleperson = new Person("Emre","Male",24,160,170);
    
    public String name;
    public String gender;
    public int age;
    public double weight;
    public double height;



    public Beverage[] drinks;

    

	public Person(String name, String gender, int age, double weight, double height)
	{
        Person x = new Person(name,gender,age,weight,height);
        this.name = name;
        this.gender = gender;
        this.age = age;
        this.weight = weight;
        this.height = height;

        
        exampleperson.SetName(name);
        exampleperson.SetGender(gender);
        exampleperson.SetAge(age);
        exampleperson.SetWeight(weight);
        exampleperson.SetHeight(height);


        exampleperson.drinks = new Beverage[100];
        exampleperson.drinks[0] = new Beverage("soft drink",50,0);
        

    }





    public double body_mass_index(Person x)
    {
        return x.weight / Math.Pow(x.height,2);
    }


    public void SetName(String name)
    {
        this.name = name;
    }
    public void SetGender(String gender)
    {
        this.gender = gender;
    }
    public void SetAge(int age)
    {
        this.age = age;
    }
    public void SetWeight(double weight)
    {
        this.weight = weight;
    }
    public void SetHeight(double height)
    {
        this.height = height;
    }

    public String GetName()
    {
        return name;
    }
    public String GetGender()
    {
        return gender;
    }
    public int GetAge()
    {
        return age;
    }
    public double GetWeight()
    {
        return weight;
    }
    public double GetHeight()
    {
        return height;
    }


}
