using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerInterface
{
    public class PlayerUI : MonoBehaviour
    {
        public static float speed = 10f;

        private Vector3 direction;
        private GameObject player;
        private Vector3 current_position;

        private Queue<Vector3> targets;
        private Vector3 curr_target;
        private bool movement_done = true;
        private int[] num_houses;

        public int player_index;

        // Start is called before the first frame update
        void Start()
        {
            player = gameObject;
            player.transform.position = new Vector3(-43.5f, -10f, 43f);
            direction = -Vector3.left;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            current_position = player.transform.position;

            targets = new Queue<Vector3>();
            StartCoroutine(MoveTowards());

            num_houses = new int[40];
            for (int i = 0; i < 40; ++i)
            {
                num_houses[i] = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }

        public void Move(int position, int roll)
        {
            if (roll == 0) return;

            if (position == 39)
            {
                current_position = new Vector3(-43.5f, -10f, 43f);
                targets.Enqueue(current_position);

                direction = -Vector3.left;
                player.transform.rotation = Quaternion.Euler(0, 90, 0) * player.transform.rotation;

                Move(0, roll - 1);
            }
            else if (position == 9)
            {
                current_position = new Vector3(43f, -10f, 43f);
                targets.Enqueue(current_position);

                direction = -Vector3.forward;
                player.transform.rotation = Quaternion.Euler(0, 90, 0) * player.transform.rotation;

                Move(10, roll - 1);
            }
            else if (position == 19)
            {
                current_position = new Vector3(43f, -10f, -43f);
                targets.Enqueue(current_position);

                direction = Vector3.left;
                player.transform.rotation = Quaternion.Euler(0, 90, 0) * player.transform.rotation;

                Move(20, roll - 1);
            }
            else if (position == 29)
            {
                current_position = new Vector3(-43f, -10f, -43f);
                targets.Enqueue(current_position);

                direction = Vector3.forward;
                player.transform.rotation = Quaternion.Euler(0, 90, 0) * player.transform.rotation;

                Move(30, roll - 1);
            }
            else
            {
                float magnitude = 8f;
                if (position == 0 || position == 10 || position == 20 || position == 30)
                {
                    magnitude = 11.5f;
                }
                current_position = current_position + direction * magnitude;
                targets.Enqueue(current_position);

                Move(position + 1, roll - 1);
            }
        }

        private IEnumerator MoveTowards()
        {
            while (true)
            {
                if (movement_done)
                {
                    if (targets.Count > 0)
                    {
                        curr_target = targets.Dequeue();
                        movement_done = false;
                    }
                    else
                    {
                        yield return null;
                    }
                }
                else
                {
                    while (Vector3.Distance(transform.position, curr_target) > 0.01f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, curr_target, speed * Time.deltaTime);
                        yield return null;
                    }

                    if (Vector3.Distance(transform.position, curr_target) <= 0.01f)
                        movement_done = true;
                }
            }
        }

        public void Purchase(int index)
        {
            if (index % 10 == 0 || index < 0 || index >= 40) throw new System.ArgumentException();

            Spawner spawner = GameObject.FindGameObjectWithTag("Script").GetComponent<Spawner>();
            Quaternion rot = Quaternion.Euler(new Vector3(-90, 0, 0));
            if (index > 0 && index < 10)
            {
                index -= 1;
                Vector3 pos = new Vector3(-34 + index * 8 + num_houses[index] * 2, -10, 38);
                spawner.SpawnHouse(player_index, pos, rot);
            }
            else if (index > 10 && index < 20)
            {
                index -= 11;
                Vector3 pos = new Vector3(38, -10, 35 - index * 8 - num_houses[index] * 2);
                spawner.SpawnHouse(player_index, pos, rot);
            }
            else if (index > 20 && index < 30)
            {
                index -= 21;
                Vector3 pos = new Vector3(30 - index * 8 - num_houses[index] * 2, -10, -38);
                spawner.SpawnHouse(player_index, pos, rot);
            }
            else if (index > 30 && index < 40)
            {
                index -= 31;
                Vector3 pos = new Vector3(-38, -10, -29 + index * 8 + num_houses[index] * 2);
                spawner.SpawnHouse(player_index, pos, rot);
            }
            num_houses[index] += 1;
        }
    }
}
