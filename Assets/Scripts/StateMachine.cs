using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IState
{
    void Enter();
    void Exit();
    void Update();
    void FixedUpdate();
    void OnCollisionEnter();
    void OnCollisionStay();
    void OnCollisionExit();
}


public class StateMachine
{
    public Dictionary<string, IState> m_states;
    public IState m_currentState;
    public List<IState> m_stack;

    public void initialize()
    {
        m_states = new Dictionary<string, IState>();
        m_stack = new List<IState>();
    }

    public void Add(string name, IState state)
    {
        m_states.Add(name, state);
    }

    public void Change(IState state)
    {
        if (m_currentState != null)
        {
            m_currentState.Exit();
        }

        m_currentState = state;

        if (m_currentState != null)
        {
            m_currentState.Enter();
        }
    }
    public void Change(string state)
    {
        Change(m_states[state]);
    }

    public void Push(IState state)
    {
        if (m_currentState != null)
        {
            m_stack.Add(m_currentState);
        }
        Change(state);
    }
    public void Push(string state)
    {
        Push(m_states[state]);
    }

    public void Pop()
    {
        if (m_currentState != null)
        {
            Change(m_stack[m_stack.Count - 1]);
            m_stack.RemoveAt(m_stack.Count - 1);
        }
        else
        {
            Debug.LogError("No current state to remove. Check that m_currentState is not null.");
        }
        
    }
}
