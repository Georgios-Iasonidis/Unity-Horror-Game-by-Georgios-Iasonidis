using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // Η ταχύτητα κίνησης του παίκτη

    public float groundDrag; // Η αντίσταση που εφαρμόζεται όταν ο παίκτης είναι στο έδαφος

    public float playerHeight; // Ύψος του παίκτη για τον έλεγχο του εδάφους
    public LayerMask WhatIsGround; // Η μάσκα για να καθορίσω τι θεωρείται έδαφος
    bool grounded; // Κατάσταση εδάφους (αν ο παίκτης είναι στο έδαφος ή όχι)
    public Transform orientation; // Η προσανατολισμένη κατεύθυνση κίνησης του παίκτη

    float horizontalInput; // Είσοδος ποντικιού για την οριζόντια κίνηση
    float verticalInput; // Είσοδος ποντικιού για την κάθετη κίνηση

    Vector3 moveDirection; // Κατεύθυνση κίνησης του παίκτη

    Rigidbody rb; // Το Rigidbody για φυσική κίνηση

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ανάθεση του Rigidbody
        rb.freezeRotation = true; // Κλείδωμα της περιστροφής του Rigidbody για να μην επηρεάζεται από φυσικές δυνάμεις
    }

    // Update is called once per frame
    void Update()
    {
        // Ελεγχος αν ο χρηστης βρισκεται στο εδαφος με raycast. (Racast = η ακτινα που ελεγχει αν συναντα αντικειμενο, 
        //transform.position = διασφαλιζει οτι το raycast ξεκιναει απο εκει που ειναι ο παικτης,
        //playerHeight * 0.5f + 0.2f = Το μηκος της ακτινας που ειναι το μεσο του υψους του παικτη + κατι παραπανω για να σιγουρευτω οτι δεν ειναι κοντη η ακτινα)
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);

        MyInput(); // Λήψη εισόδου από τον χρήστη
        SpeedControl(); // Ρύθμιση ταχύτητας κίνησης

        // Εφαρμογή - χειρίζομαι την αντίσταση
        if (grounded)
            rb.drag = groundDrag; // Αν ο παίκτης είναι στο έδαφος, εφαρμόζω την αντίσταση
        else
            rb.drag = 0; // Αν ο παίκτης δεν είναι στο έδαφος, δεν εφαρμόζω αντίσταση
    }

    //fixedupdate = ελεγχος κινησης - σταθερα χρονικα διαστηματα, ανεξαρτητα τα framerates 
    void FixedUpdate() 
    {
        MovePlayer(); // Κίνηση του παίκτη
    }

    void MyInput()
    {
        // Λήψη των εισόδων από το πληκτρολογιο (βελακια ή awds) για την κίνηση
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        // Υπολογίζω την κατεύθυνση κίνησης - πάντα κίνηση προς την κατεύθυνση που κοιτάω
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Ορίζω την ταχύτητα του παίκτη απευθείας, διατηρώντας την υπάρχουσα ταχύτητα στον άξονα Υ (για τη βαρύτητα)
        rb.velocity = new Vector3(moveDirection.normalized.x * moveSpeed, rb.velocity.y, moveDirection.normalized.z * moveSpeed);
    }

    //Μεθοδος που διασφαλιζει οτι η ταχυτητα δε ξεπερνα την καθορισμενη movespeed
    void SpeedControl()
    {   
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //η συνολική ταχύτητα

        // Περιορίζω την ταχύτητα αν χρειάζεται
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
