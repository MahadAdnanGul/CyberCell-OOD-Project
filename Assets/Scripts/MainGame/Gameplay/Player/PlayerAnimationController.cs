using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Gameplay.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator animator;
        private string currentAnimation = "Idle";

        private string attackAnimation = "RunAttack";

        private Dictionary<string, AnimationClip> clipDictionary = new Dictionary<string, AnimationClip>();

        private void InitializeAnimator()
        {
            RuntimeAnimatorController animController = animator.runtimeAnimatorController;
            AnimationClip[] animClips = animController.animationClips;
            foreach (AnimationClip clip in animClips)
            {
                clipDictionary.Add(clip.name,clip);
            }
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            InitializeAnimator();
        }

        public float GetCurrentAnimationDuration()
        {
            return clipDictionary[currentAnimation].length;
        }
        public bool SetAnimation(string animName)
        {
            if (!currentAnimation.Equals(animName))
            {
                animator.CrossFade(animName,0.2f);
                currentAnimation = animName;
                return true;
            }

            return false;
        }
    }
}
