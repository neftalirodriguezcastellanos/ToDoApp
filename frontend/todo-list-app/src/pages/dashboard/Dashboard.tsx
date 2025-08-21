import { Box, Container, Fab, List } from "@mui/material";
import { useEffect } from "react";
import useDashboardHandler from "./DashboardHandler";
import CustomListItem from "../../components/CustomListItem";
import AddIcon from "@mui/icons-material/Add";
import TaskFormModal from "../../components/TaskFormModal";
import ConfirmDialog from "../../components/ConfirmDialog";

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
  } = useDashboardHandler();

  useEffect(() => {
    getTasks();
  }, []);

  return (
    <Container maxWidth="lg">
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          minHeight: "100vh",
          justifyContent: "center",
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
            />
          ))}
        </List>
        <Fab color="primary" aria-label="add" onClick={handleCreateTask}>
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
  );
};

export default Dashboard;
