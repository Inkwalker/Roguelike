using UnityEngine;

namespace Roguelike
{
    public class CameraController : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] KeyCode rotateLeftKey;
        [SerializeField] KeyCode rotateRightKey;
        [SerializeField] float rotationSpeed = 90;

        [Header("Movement")]
        [SerializeField] float movementFriction = .1f;

        [Header("Zoom")]
        [SerializeField] float minZoom = 5f;
        [SerializeField] float maxZoom = 10f;
        [SerializeField] float zoomFriction;
        [SerializeField] float zoomSpeed = 0.1f;

        [Header("Links")]
        [SerializeField] new Camera camera;

        bool inputEnabled = true;
        Bounds boundingBox;

        Vector3 cameraVelocity;
        bool isDraging;
        Vector3 oldMousePosition;

        Quaternion rotationTarget;
        Quaternion rotationStart;
        float rotationTime;
        float rotationTimer = 0;
        bool rotating = false;

        float zoomVelocity;

        public void EnableInput()
        {
            inputEnabled = true;
        }

        public void DisableInput()
        {
            inputEnabled = false;
        }

        void Start()
        {
            boundingBox = new Bounds(new Vector3(45, 0, 45), new Vector3(90, 2, 90));

            transform.position = new Vector3(
                boundingBox.center.x,
                0,
                boundingBox.center.z
            );

            rotationTarget = transform.rotation;
        }

        void Update()
        {
            if (inputEnabled == false)
            {
                return;
            }

            Move();
            Rotate();
            Zoom();
        }

        void Move()
        {
            if (Input.GetMouseButton(1))
            {
                Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
                Plane ground = new Plane(Vector3.up, Vector3.zero);

                float d;
                if (ground.Raycast(mouseRay, out d))
                {
                    Vector3 position = mouseRay.GetPoint(d);

                    if (isDraging)
                    {
                        Vector3 delta = oldMousePosition - position;
                        Vector3 to = transform.position + delta;
                        transform.position = boundingBox.ClosestPoint(to);

                        cameraVelocity = delta / Time.deltaTime;
                    }
                    else
                    {
                        oldMousePosition = position;
                        isDraging = true;
                        //TileCursor.Lock();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonUp(1))
                {
                    isDraging = false;
                    //TileCursor.Unlock();
                }

                cameraVelocity -= cameraVelocity * movementFriction * Time.deltaTime;
                Vector3 to = transform.position + cameraVelocity * Time.deltaTime;
                transform.position = boundingBox.ClosestPoint(to);
            }
        }

        void Rotate()
        {
            bool rotateLeft = Input.GetKeyDown(rotateLeftKey);
            bool rotateRight = Input.GetKeyDown(rotateRightKey);

            if (rotateLeft || rotateRight)
            {
                //if (!rotating) TileCursor.Lock();

                float angle = rotateLeft ? 90 : -90;
                rotationTarget *= Quaternion.AngleAxis(angle, Vector3.up);
                rotationTime = Quaternion.Angle(rotationTarget, transform.rotation) / rotationSpeed;
                rotationTimer = 0;
                rotationStart = transform.rotation;

                rotating = true;
            }

            if (rotating)
            {
                float t = rotationTimer / rotationTime;
                t = Mathf.Clamp01(t);
                t = Mathf.Sin(t * Mathf.PI * 0.5f);

                rotationTimer += Time.deltaTime;

                transform.rotation = Quaternion.Lerp(rotationStart, rotationTarget, t);

                if (rotationTimer / rotationTime >= 1f)
                {
                    rotating = false;
                    //TileCursor.Unlock();
                }
            }
        }

        void Zoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0)
            {
                zoomVelocity = -scroll * zoomSpeed / Time.deltaTime;
            }

            camera.transform.Translate(Vector3.back * zoomVelocity, Space.Self);

            zoomVelocity -= zoomVelocity * zoomFriction * Time.deltaTime;
        }
    }
}
