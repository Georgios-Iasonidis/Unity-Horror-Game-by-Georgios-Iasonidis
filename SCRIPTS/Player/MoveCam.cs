using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;  // Η θέση της κάμερας που θέλω να ακολουθήσω

    // Η συνάρτηση Update εκτελείται μία φορά ανά frame
    void Update()
    {
        // Ρυθμίζω τη θέση αυτού του αντικειμένου να ταιριάζει με τη θέση της κάμερας
        transform.position = cameraPosition.position;
    }
}
