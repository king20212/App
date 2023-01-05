import React, { useState, useEffect } from 'react';
import AddApproverModal from './Component/AddApproverModal';
import EditApproverModal from './Component/EditApproverModal';

export default function BTAApprover() {
    const [btaApprovers, setBTAApprovers] = useState([]);
    const [blnAddModal, setBlnAddModal] = useState(false);
    const [blnEditModal, setBlnEditModal] = useState(false);
    const [editingApprover, setEditingApprover] = useState(null);

    function refreshList() {
        fetch('https://localhost:7195/api/BTAApprovers')
            .then(response => response.json())
            .then(data => setBTAApprovers(data));
    }

    function onPostCreate(createdPost) {
        if (createdPost === null) {
            return;
        }

        alert("Created Successfully");
    }

    function onDeleted(deletedid) {
        let btaApproversCopy = [...btaApprovers];

        const index = btaApproversCopy.findIndex((btaApproversCopyapprover, currentIndex) => {
            if (btaApproversCopyapprover.BTA_APPROVER_ID === deletedid) {
                return true;
            }
        });

        if (index !== -1) {
            btaApproversCopy.splice(index, 1);
        }

        setBTAApprovers(btaApproversCopy);
    }

    function addModalClose() {
        setBlnAddModal(false);
    }

    function editModalClose(postToUpdate) {
        setBlnEditModal(false);

        if (postToUpdate === null) {
            return;
        }

        let btaApproversCopy = [...btaApprovers];

        const index = btaApproversCopy.findIndex((btaApproversCopyapprover, currentIndex) => {
            if (btaApproversCopyapprover.BTA_APPROVER_ID === postToUpdate.BTA_APPROVER_ID) {
                return true;
            }
        });

        if (index !== -1) {
            btaApproversCopy[index] = postToUpdate;
        }

        setBTAApprovers(btaApproversCopy);
    }

    function handleChange(event) {
        setEditingApprover({
            ...editingApprover,
            [event.target.name]: event.target.value,
        });
    };

    useEffect(() => {
        refreshList();
    })

    function deleteApprover(btaapproverid) {
        if (window.confirm("Confrim to delete?")) {
            console.log(btaapproverid);
            fetch('https://localhost:7195/api/BTAApprovers/' + btaapproverid, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
            .then(res => res.json())
            .then((result) => {
                alert(result);
                onDeleted(btaapproverid)
            }, (error) => {
                alert(error);
                console.log(error);
            });
        }
    }

    return (
        <div className="table-responsive mt-5">
            <table className="table table-bordered border-dark">
                <thead>
                    <tr>
                        <th scope="col">User</th>
                        <th scope="col">Sequence</th>
                        <th scope="col">Approver</th>
                        <th scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>
                    {btaApprovers.map((btaapprover) => (                     
                        <tr key={btaapprover.BTA_APPROVER_ID}>
                            <td>{btaapprover.USER_NAME}</td>
                            <td>{btaapprover.SEQ_NO}</td>
                            <td>{btaapprover.APPROVER_NAME}</td>
                            <td>
                                <div className="btn-group" role="group">
                                    <button type="button" className="btn btn-secondary" onClick={() => {
                                        setBlnEditModal(true);
                                        setEditingApprover(btaapprover);
                                    }
                                    } >Edit</button>
                                    {editingApprover !== null && <EditApproverModal show={blnEditModal} onHide={editModalClose} btaapprover={editingApprover} handleChange={handleChange} ></EditApproverModal>}
                                    <button type="button" className="btn btn-danger" onClick={() => { deleteApprover(btaapprover.BTA_APPROVER_ID) } } >Delete</button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div className="btn-group" role="group" aria-label="Basic example">
                <button type="button" className="btn btn-primary" onClick={() => setBlnAddModal(true)}>Add Approver</button>
                {blnAddModal && <AddApproverModal show={blnAddModal} onHide={addModalClose}></AddApproverModal>}
            </div>
        </div>
    );
}