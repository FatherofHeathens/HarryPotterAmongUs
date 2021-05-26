using System;
using hunterlib.Classes;
using InnerNet;
using UnityEngine;

namespace HarryPotter.Classes
{
    public delegate void HitEvent(Spell spell, PlayerControl player);
    
    [RegisterInIl2Cpp]
    public class Spell : MonoBehaviour
    {
        public ModdedPlayerClass Owner { get; set; }
        public Vector3 MousePostition { get; set; }
        public SpriteRenderer SpellRender { get; set; }
        public Rigidbody2D SpellRigid { get; set; }
        public CircleCollider2D SpellCollider { get; set; }
        public DateTime ShootTime { get; set; }
        public Sprite[] SpellSprites { get; set; }
        
        public event HitEvent OnHit;
        private int _spriteIndex;
        
        public Spell(IntPtr ptr) : base(ptr)
        {
        }

        public void Start()
        {
            ShootTime = DateTime.UtcNow;
            
            SpellRender = gameObject.AddComponent<SpriteRenderer>();
            SpellRigid = gameObject.AddComponent<Rigidbody2D>();
            SpellCollider = gameObject.AddComponent<CircleCollider2D>();
            
            gameObject.SetActive(true);
            SpellRender.enabled = true;
            
            SpellRigid.transform.position = Owner._Object.myRend.bounds.center;
            SpellRender.transform.localScale = new Vector2(1f, 1f);

            Vector3 v = MousePostition - Owner._Object.myRend.bounds.center;
            float dist = Vector2.Distance(MousePostition, Owner._Object.myRend.bounds.center);
            Vector3 d = v * 3f * (2f / dist);
            float AngleRad = Mathf.Atan2(MousePostition.y - Owner._Object.myRend.bounds.center.y, MousePostition.x - Owner._Object.myRend.bounds.center.x);
            float shootDeg = (180 / (float)Math.PI) * AngleRad;

            SpellCollider.isTrigger = true;
            SpellCollider.radius = 0.2f;
            SpellRigid.velocity = new Vector2(d.x, d.y);
            gameObject.layer = 31;

            SpellRigid.rotation = shootDeg;
            SpellRigid.drag = 0;
            SpellRigid.angularDrag = 0;
            SpellRigid.inertia = 0;
            SpellRigid.gravityScale = 0;
        }

        public void Update()
        {
            if (_spriteIndex <= 5)
                SpellRender.sprite = SpellSprites[0];
            else
                SpellRender.sprite = SpellSprites[1];

            if (_spriteIndex >= 10)
                _spriteIndex = 0;

            _spriteIndex++;

            if (Owner._Object.AmOwner)
            {
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (player.Data.IsDead || player.Data.Disconnected || Owner._Object == player || player.Data.IsImpostor)
                        continue;

                    if (!player.myRend.bounds.Intersects(SpellRender.bounds))
                        continue;

                    if (!player.Collider.enabled)
                        continue;
                        
                    OnHit?.Invoke(this, player);
                    return;
                }
            }

            if (ShootTime.AddSeconds(5) < DateTime.UtcNow ||
                MeetingHud.Instance ||
                !AmongUsClient.Instance.IsGameStarted)
            {
                gameObject.Destroy();
            }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            switch (other.gameObject.layer)
            {
                case 10:
                case 11:
                    OnHit?.Invoke(this, null);
                    break;
            }
        }
    }
}