using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class Envelope : MonoBehaviour
{
   private Animator m_animator;
   private static readonly int OpenEnvelope1 = Animator.StringToHash("OpenEnvelope");

   private Invoice m_invoiceToOpen;

   private void Awake()
   {
      m_animator = GetComponent<Animator>();
      gameObject.SetActive(false);
   }

   public void OpenEnvelope(Invoice invoice)
   {
      m_invoiceToOpen = invoice;
      gameObject.SetActive(true);
      GameEventManager.TriggerEvent(new GameEvent_InvoiceOpen(true));
      m_animator.SetTrigger(OpenEnvelope1);
   }

   public void OnAnimationFinished()
   {
      gameObject.SetActive(false);
   }

   public void ShowInvoice()
   {
      m_invoiceToOpen.ShowInvoice();
      m_invoiceToOpen = null;
   }
}
