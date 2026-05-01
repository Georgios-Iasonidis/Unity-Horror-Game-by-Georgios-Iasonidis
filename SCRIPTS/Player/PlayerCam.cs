using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX; // περιστροφή στον άξονα Χ - sensitivity
    public float sensY; // περιστροφή στον άξονα Υ - sensitivity

    public Transform orientation; // Η αναφορά στον προσανατολισμό (χρησιμοποιείται για την περιστροφή του σώματος του παίκτη)

    private float xRotation; // Η περιστροφή γύρω από τον άξονα Χ (για την κίνηση πάνω-κάτω)
    private float yRotation; // Η περιστροφή γύρω από τον άξονα Υ (για την κίνηση αριστερά-δεξιά)

    // Μεταβλητές για την κάθετη ταλάντωση
    public float yOffsetAmplitude = 0.004f; // Το πλάτος της κάθετης ταλάντωσης
    public float yOffsetSpeed = 2f; // Η ταχύτητα της κάθετης ταλάντωσης

    private Vector3 initialCameraPosition; // Μεταβλητη της αρχικής θέσης της κάμερας

    void Start()
    {
        // Κλειδώνω τον κέρσορα στο κέντρο της οθόνης και τον αποκρύπτω (το κανω για να μη φευγει ο κερσορας απο τη θεση του οταν μετακινω το ποντικι)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Αποθηκεύω την αρχική θέση της κάμερας
        initialCameraPosition = transform.localPosition;

        // Αρχικοποιώ την περιστροφή στον άξονα Υ στις 90 μοίρες για να ξεκινήσει η κάμερα με τη σωστή κατεύθυνση
        yRotation = 90f;
    }

    void Update()
    {
        // Διαβάζω τις εισόδους του ποντικιού
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Ενημερώνω τις περιστροφές βαση των εισοδων, αρα η κινηση του ποντικιου επηρεαζει την κατευθυνση της καμερας
        yRotation += mouseX;
        xRotation -= mouseY;

        // Περιορίζω την περιστροφή πανω και κατω (κοιταει απο το ταβανι εως μεχρι και τα ποδια)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Εφαρμόζω το εφέ της κάθετης ταλάντωσης
        float yOffset = Mathf.Sin(Time.time * yOffsetSpeed) * yOffsetAmplitude;
        Vector3 newCameraPosition = initialCameraPosition + new Vector3(0, yOffset, 0);
        transform.localPosition = newCameraPosition;

        // Εφαρμόζω τις περιστροφές στην κάμερα
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // Μέθοδος για εξωτερική προσαρμογή της περιστροφής της κάμερας στον άξονα Υ (χρισημοποιειται για αλληλεπιδρασεις της καμερας του παικτη με αλλα αντικειμενα οταν κι αν αυτα ενεργοποιηθουν)
    public void AdjustYRotation(float adjustment)
    {
        yRotation += adjustment;
    }
}
