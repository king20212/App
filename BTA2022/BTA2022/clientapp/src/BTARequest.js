import React, { useState, useEffect } from 'react';
import AddRequest from "./Component/AddRequest";
import EditRequest from "./Component/EditRequest";

export default function BTARequest() {
    const [btaRequests, setBTARequests] = useState([]);
    const [blnAddRequest, setBlnAddRequest] = useState(false);
    const [blnEditRequest, setBlnEditRequest] = useState(false);
    const [editingRequest, setEditingRequest] = useState(null);

    function refreshList() {
        fetch('https://localhost:7195/api/BTARequests')
            .then(response => response.json())
            .then(data => setBTARequests(data));
    }

    useEffect(() => {
        refreshList();
    }, [])


    function addRequestClose() {
        setBlnAddRequest(false);
    }

    function editRequestClose() {
        setBlnEditRequest(false);
    }


    return (
        <div className="table-responsive m-5">
            {blnAddRequest === false && blnEditRequest === false && 

            (<div>
                <table className="table table-striped">
                    <thead className="table-dark">
                        <tr>
                            <th scope="col">Name of Traveler</th>
                            <th scope="col">Request Date</th>
                            <th scope="col">Request Status</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                <tbody>
                    {btaRequests.map((btaRequest) => (
                        <tr key={btaRequest.REQUEST_ID}>
                            <td>{btaRequest.USER_NAME}</td>
                            <td>{btaRequest.REQUEST_DATE}</td>
                            <td>{btaRequest.STATUS_DESC}</td>
                            <td>
                                <div className="btn-group" role="group">
                                    <button type="button" className="btn btn-secondary" onClick={() => {
                                        setBlnEditRequest(true);
                                        setEditingRequest(btaRequest);
                                    }
                                    }>Edit</button>
                                    <button type="button" className="btn btn-danger">Delete</button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div className="btn-group" role="group" aria-label="Basic example">
                <button type="button" className="btn btn-primary" onClick={() => {setBlnAddRequest(true)} }>New Request</button>
                </div> 
                </div>
                )
            }
            {blnAddRequest && <AddRequest onHide={addRequestClose}></AddRequest>}
            {blnEditRequest && <EditRequest onHide={editRequestClose} btareq={editingRequest}></EditRequest>}
        </div>
        
    );
}