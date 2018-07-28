using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EasyDialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialogueHolder;

        [Space(15)] public NarrativeSO narrativeSo;
        public Text narrativeText;
        public Image characterIcon;
        
        [Header("Interact Key")] public KeyCode interactionKey = KeyCode.E;
        public bool enableSkip;
        public float lineVisibleTime = 3f;
        

        private string currentText = String.Empty;
        private Text dialogueText;
        private Queue<string> Sentences;
        private bool isSkipped = false;
        private Coroutine co;
        private Coroutine skipCo;
        

        private void Awake() {
            Sentences = new Queue<string>();
        }

        // Use this for initialization
        private void Start() {
            StartNarrative(narrativeSo.narrative.text, narrativeSo.narrative.characterIcon);
        }

        private void Update() {
            //player can skip the current line
            if (enableSkip) {
                if (Input.GetKeyDown(interactionKey)) {
                    if (isSkipped == false) {
                        SkipEffect();
                    }
                    else {
                        if (co != null) {
                            isSkipped = false;
                            RefreshDialogue();
                        }
                    }

                }
            }
        }//end update
        

        public void StartNarrative(string[] narratives, Sprite charIcon) {
            dialogueHolder.SetActive(true);
            Sentences.Clear();

            characterIcon.sprite = charIcon;
            dialogueText = narrativeText;

            foreach (var text in narratives) Sentences.Enqueue(text);

            RefreshDialogue();
        }

        public void SkipEffect() {
            isSkipped = true;
            if (co != null) {
                StopCoroutine(co);
            }
            
            dialogueText.text = currentText;

        }

        public void RefreshDialogue() {
            if (Sentences.Count == 0) {
                EndDialogue();
                return;
            }

            Debug.Log("next line dialogue");
            currentText = Sentences.Dequeue();
            if (co != null)
                StopCoroutine(co);

            co = StartCoroutine(TextEffect(currentText));
        }


        private IEnumerator TextEffect(string sentence) {
            dialogueText.text = "";
            foreach (var letter in sentence) {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.025f);
            }

            yield return new WaitForSeconds(lineVisibleTime);
        
            RefreshDialogue();
        }
        
        
        private void EndDialogue() {
            DisableUI();
            //Debug.Log("dialogue ended");
        }

        private void DisableUI() {
            dialogueHolder.SetActive(false);
        }
        
    }
}