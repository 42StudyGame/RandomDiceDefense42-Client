using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillHolder : MonoBehaviour
{
    public Skill skill;

    enum skillState
    {
        ready,
        active,
        cooldown
    }
    skillState state = skillState.ready;

    private void OnEnable()
    {
        if (skill != null)
        {
            StartCoroutine(ActivateSkill());
        }
    }

    IEnumerator ActivateSkill()
    {
        Debug.Log("Skill use");
        skill.Activate(gameObject);
        yield return new WaitForSeconds(skill.activeTime);
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(CoolDowningSkill());
        }
            
    }

    IEnumerator CoolDowningSkill()
    {
        Debug.Log("Start CoolDown");
        skill.BeginCoolDown(gameObject);
        yield return new WaitForSeconds (skill.cooldownTime);
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(ActivateSkill());
        }
            
    }
    /*
    private void Update()
    {
        switch(state)
        {
            case skillState.ready:
                skill.Activate(gameObject);
                state = skillState.active;
                activeTime = skill.activeTime;
                break;
            case skillState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    skill.BeginCoolDown(gameObject);
                    state = skillState.ready;
                    cooldownTime = skill.cooldownTime;
                }
                break;
            case skillState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = skillState.ready;
                }
                break;
        }
    }*/
}
