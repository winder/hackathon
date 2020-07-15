package com.algorand.javatest;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public final class Person {
    //{ firstName: "John", lastName: "Doe", age: 50, eyeColor: "blue" };
    private final String firstName;
    private final String lastName;
    private final int age;
    private final String eyeColor;
    
    @JsonCreator
    public Person(@JsonProperty("firstName") String firstName, @JsonProperty("lastName") String lastName, @JsonProperty("age") int age, @JsonProperty("eyeColor") String eyeColor ) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.eyeColor = eyeColor;
    }

    public String getFirstName() {
        return firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public int getAge() {
        return age;
    }

    public String getEyeColor() {
        return eyeColor;
    }

}