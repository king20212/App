import Home from "./Home";
import BTAApprover from "./BTAApprover";
import BTARequest from "./BTARequest";
import BTAApproval from "./BTAApproval";
import Login from "./Login";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/btarequest',
        element: <BTARequest />
    },
    {
        path: '/btaapproval',
        element: <BTAApproval />
    },
    {
        path: '/btaapprover',
        element: <BTAApprover />
    }
];

export default AppRoutes;
