import React, { useState, useEffect } from 'react';
import ViewRequest from "./Component/ViewRequest";

export default function BTAApproval() {
    const [btaRequests, setBTARequests] = useState([]);
    const [blnViewRequest, setBlnViewRequest] = useState(false);
    const [viewingRequest, setViewingRequest] = useState(null);

    function refreshList() {
        fetch('https://localhost:7195/api/BTARequests/GetPendingApprovalRequests')
            .then(response => response.json())
            .then(data => setBTARequests(data));
    }

    useEffect(() => {
        refreshList();
    }, [])


    function viewRequestClose() {
        setBlnViewRequest(false);
    }

    function onApprove(id) {
        if (window.confirm("Confrim to approve?")) {
            fetch('https://localhost:7195/api/BTARequests/ApproveRequest/' + id, {
                method: 'PATCH',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(res => res.json())
                .then((result) => {
                    alert(result);
                }, (error) => {
                    alert(error);
                });
        }
    }

    function onReject(id) {
        if (window.confirm("Confrim to reject?")) {
            fetch('https://localhost:7195/api/BTARequests/RejectRequest/' + id, {
                method: 'PATCH',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(res => res.json())
                .then((result) => {
                    alert(result);
                }, (error) => {
                    alert(error);
                });
        }
    }


    return (
        <div className="table-responsive m-5">
            {blnViewRequest === false &&

                (<div>
                <table className="table table-striped">
                    <thead className="table-dark">
                            <tr>
                                <th scope="col">Name of Traveler</th>
                                <th scope="col">Department</th>
                                <th scope="col">Phone Number</th>
                                <th scope="col">Request Date</th>
                                <th scope="col">Request Status</th>
                                <th scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {btaRequests.map((btaRequest) => (
                                <tr key={btaRequest.REQUEST_ID}>
                                    <td>{btaRequest.USER_NAME}</td>
                                    <td>{btaRequest.DEPARTMENT}</td>
                                    <td>{btaRequest.EXTN_NO}</td>
                                    <td>{btaRequest.REQUEST_DATE}</td>
                                    <td>{btaRequest.REQUEST_STATUS}</td>
                                    <td>
                                        <div className="btn-group" role="group">
                                            <button type="button" className="btn btn-primary" onClick={() => {
                                                setBlnViewRequest(true);
                                                setViewingRequest(btaRequest);
                                            }
                                            }>View</button>
                                            <button type="button" className="btn btn-success" onClick={() => onApprove(btaRequest.REQUEST_ID)}>Approve</button>
                                            <button type="button" className="btn btn-danger" onClick={() => onReject(btaRequest.REQUEST_ID)}>Reject</button>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
                )
            }
            {blnViewRequest && <ViewRequest onHide={viewRequestClose} btareq={viewingRequest}></ViewRequest>}
        </div>

    );
}