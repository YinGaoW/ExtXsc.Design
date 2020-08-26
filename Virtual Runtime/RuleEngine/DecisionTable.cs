using System.Collections;
using Eap;

namespace ExtEap
{
    public class DecisionTable : Statement
    {
        private ComponentArrayListDecorator m_componentarraylistdecoratorDecisionTableMember;
        public DecisionTable(Statement statementOwner) : base(statementOwner)
        {
            OnConstruct();
        }
        public DecisionTable(Statement statementOwner, Method method) : base(statementOwner, method)
        {
            OnConstruct();
        }

        public int DecisionTableMemberCount
        {
            get { return m_componentarraylistdecoratorDecisionTableMember.ComponentCount; }
        }

        private void OnConstruct()
        {
            m_componentarraylistdecoratorDecisionTableMember = new ComponentArrayListDecorator(this, ComponentFactory, typeof(DecisionTableMember), new object[] { this, Method });
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_componentarraylistdecoratorDecisionTableMember.Initialize();

            OnConstruct();
        }

        public DecisionTableMember GetDecisionTableMember(int nIndex)
        {
            return (DecisionTableMember)m_componentarraylistdecoratorDecisionTableMember.GetComponent(nIndex);
        }
        public void SetDecisionTableMember(int nIndex, DecisionTableMember decisiontablemember, bool bIsInserting)
        {
            m_componentarraylistdecoratorDecisionTableMember.SetComponent(nIndex, decisiontablemember, bIsInserting);
        }
        public void SetDecisionTableMember(int nIndex, DecisionTableMember decisiontablemember)
        {
            m_componentarraylistdecoratorDecisionTableMember.SetComponent(nIndex, decisiontablemember);
        }

        protected override Object OnExecute(AdmlObject admlobject, ExecutionStack executionstack)
        {
            int nIndex;
            string strExecutionName = executionstack.GetExecutionName();

            DecisionTableMember[] decisiontablemember = new DecisionTableMember[DecisionTableMemberCount];

            for (int i = 0; i < decisiontablemember.Length; i++)
                decisiontablemember[i] = GetDecisionTableMember(i);

            QuickSort(decisiontablemember, 0, decisiontablemember.Length - 1);

            if (strExecutionName != null)
            {
                nIndex = int.Parse(strExecutionName.Substring(20));

                goto DecisionTableMember;
            }

            nIndex = 0;

        IsEnd:

            if (nIndex >= DecisionTableMemberCount)
                goto End;

            executionstack.PushExecutionName("DecisionTableMember:" + nIndex);

        DecisionTableMember:

            Object objectReturn = decisiontablemember[nIndex].Execute(admlobject, executionstack);

            executionstack.PopExecutionName();

            if (objectReturn != null)
            {
                if (objectReturn is ReferableObject)
                    executionstack.ReturnObjectReference.CreateReference((ReferableObject)objectReturn);

                return objectReturn;
            }

            nIndex++;
            goto IsEnd;

            End:
                return base.OnExecute(admlobject, executionstack);
        }
        protected override void OnParse(SyntaxErrorList syntaxerrorlist)
        {
            for (int nIndex = 0; nIndex < DecisionTableMemberCount; nIndex++)
                GetDecisionTableMember(nIndex).Parse(syntaxerrorlist);
        }
        private void QuickSort(DecisionTableMember[] decisiontablemember, int left, int right) // 快速排序，按DecisionTableMember的Priority排序，值越小优先级越高，从0开始
        {
            if (left < right)
            {
                int i = left, j = right;
                DecisionTableMember key = decisiontablemember[left]; // 基准
                
                while (i < j)
                {
                    while (i < j && decisiontablemember[j].Priority >= key.Priority) // 从右向左找第一个小于基准key的数
                        j--;

                    if (i < j) // 在右边找到了比基准key小的数
                        decisiontablemember[i++] = decisiontablemember[j];

                    while (i < j && decisiontablemember[i].Priority < key.Priority) // 从左向右找第一个大于基准key的数
                        i++;

                    if (i < j) // 在左边找到了比基准key大的数
                        decisiontablemember[j--] = decisiontablemember[i];
                }

                decisiontablemember[i] = key;
                QuickSort(decisiontablemember, left, i - 1);
                QuickSort(decisiontablemember, i + 1, right - 1);
            }
        }
    }
}
