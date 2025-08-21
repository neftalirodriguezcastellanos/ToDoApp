import {
  AppBar,
  Toolbar,
  Typography,
  Button,
  Box,
  Container,
  Fab,
  List,
} from "@mui/material";
import { useEffect } from "react";
import useDashboardHandler from "./DashboardHandler";
import CustomListItem from "../../components/CustomListItem";
import AddIcon from "@mui/icons-material/Add";
import TaskFormModal from "../../components/TaskFormModal";
import ConfirmDialog from "../../components/ConfirmDialog";
import { useAuth } from "../../context/AuthContext";

const Dashboard = () => {
  const {
    tasksList,
    getTasks,
    openModal,
    setOpenModal,
    handleCreateTask,
    handleUpdateTask,
    handleConfirmDelete,
    task,
    openConfirmDialog,
    setOpenConfirmDialog,
    handleDeleteTask,
    handleChangeCheck,
  } = useDashboardHandler();
  const { logout, fullName } = useAuth();

  const handleLogout = () => {
    logout();
  };

  useEffect(() => {
    getTasks();
  }, []);

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static" sx={{ mb: 3 }}>
        <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
          <Typography variant="h6" sx={{ fontWeight: "bold" }}>
            Mis Tareas
          </Typography>
          <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
            <Typography variant="body1">Hola, {fullName}</Typography>
            <Button color="inherit" onClick={handleLogout}>
              Logout
            </Button>
          </Box>
        </Toolbar>
      </AppBar>

      <Container maxWidth="lg">
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            minHeight: "100vh",
            justifyContent: "flex-start",
            alignItems: "center",
            px: 2,
          }}
        >
          <List sx={{ width: 500 }}>
            {tasksList.map((_task) => (
              <CustomListItem
                key={_task.id}
                task={_task}
                onEdit={() => handleUpdateTask(_task)}
                onDelete={() => {
                  handleConfirmDelete(_task);
                }}
                onCheck={handleChangeCheck}
              />
            ))}
          </List>

          <Fab
            color="primary"
            aria-label="add"
            onClick={handleCreateTask}
            sx={{ position: "fixed", bottom: 24, right: 24 }}
          >
            <AddIcon />
          </Fab>
        </Box>

        {openModal && (
          <TaskFormModal
            open={openModal}
            onClose={() => setOpenModal(false)}
            onTaskCreated={getTasks}
            task={task ?? undefined}
          />
        )}

        {openConfirmDialog && (
          <ConfirmDialog
            open={openConfirmDialog}
            onCancel={() => setOpenConfirmDialog(false)}
            onConfirm={() => {
              handleDeleteTask();
            }}
          />
        )}
      </Container>
    </Box>
  );
};

export default Dashboard;
