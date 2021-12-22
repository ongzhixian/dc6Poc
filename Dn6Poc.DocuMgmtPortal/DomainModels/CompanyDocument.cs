namespace Dn6Poc.DocuMgmtPortal.DomainModels
{
    public class CompanyDocument
    {
        public IList<CompanyDocumentPage> Pages { get; set; }

        public CompanyDocument()
        {
            Pages = new List<CompanyDocumentPage>();
        }
    }

    public class CompanyDocumentPage
    {

    }
}
