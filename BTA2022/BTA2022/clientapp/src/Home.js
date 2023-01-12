export default function Home() {
    

    return (
        <div className="container">
            <div className="row min-vh-100">
                <div className="col d-flex flex-column justify-content-center align-items-center">
                    <h1>This is a simple Business Travel Approval (BTA) System</h1>
                    <p>The system consists of 3 role functions</p>
                    <p>Requestor: can submit new business travel request; view submitted business travel requests on My BTA Request page</p>
                    <p>Approver: can review business travel request and approved/rejected the requests required his/her approval through Awaiting Approval page</p>
                    <p>Admin: can manage the approval relation for requestor and approver through BTA Approver page</p>
                </div>
            </div>

        </div>
    );
}