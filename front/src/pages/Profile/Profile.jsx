import { Container } from '@mui/material';
import MyProfile from '../../components/MyProfile';
import MyAppartment from '../../components/MyAppartment';

const Profile = () => {
  return (
    <Container sx={{ m: 'auto', height: '370' }}>
      <MyProfile />
      <MyAppartment />
    </Container>
  );
};

export default Profile;
