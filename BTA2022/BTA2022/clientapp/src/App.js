import React, { useState } from "react";
import Navigation from "./Navigation";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import AppRoutes from './AppRoutes';
import Login from "./Login";

function App() {
    const [userContext, setUserContext] = useState();

    function onLogin(userID, password) {
        fetch('https://localhost:7195/api/BTAUserAuth/UserAuth?UserID=' + userID + '&Password=' + password)
            .then(response => response.json())
            .then(data => setUserContext(data));
    }

    return (
        <div>
            {!userContext && <Login onLogin={onLogin}></Login>}
            {userContext && <BrowserRouter>
                <Navigation />
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </BrowserRouter>}
        </div>
    );
}

export default App;
