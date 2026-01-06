using BusinessLogicLayer.Services.Contracts;

namespace LibaryMangUI.Forms
{
    internal class MemberForm : MemberMenu
    {
        private IMemberService memberService;

        public MemberForm(IMemberService memberService)
        {
            this.memberService = memberService;
        }
    }
}