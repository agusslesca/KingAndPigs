using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance; //ya tener la instancia con estas lineas de codigo ya puedo acceder en otros lugares
    [SerializeField] private PlayerController _playerController;
    public PlayerController PlayerController { get => _playerController; }
    

    [SerializeField] private int _diamondCollected;
    public int DiamondCollected { get => _diamondCollected; }

    private void Awake()
    {
       if (instance == null) instance =this;
       else Destroy(gameObject);
    }

    public void AddDiamond () => _diamondCollected ++; // si pongo => es para poder hacer linea de codigos de pocas o una linea, sin tener que hacer para abajo con los {}



   


}
