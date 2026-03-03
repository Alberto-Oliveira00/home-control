import { useNavigate, useLocation } from 'react-router-dom';
import { Layout, Menu } from 'antd';
import { DashboardOutlined, UserOutlined, TagsOutlined, TransactionOutlined } from '@ant-design/icons';

const { Sider } = Layout;

export default function Sidebar() {
  const navigate = useNavigate();
  const location = useLocation();

  const menuItems = [
    { key: '/', icon: <DashboardOutlined />, label: 'Dashboard' },
    { key: '/users', icon: <UserOutlined />, label: 'Pessoas' },
    { key: '/categories', icon: <TagsOutlined />, label: 'Categorias' },
    { key: '/transactions', icon: <TransactionOutlined />, label: 'Transações' },
  ];

  return (
    <Sider breakpoint="lg" collapsedWidth="0">
      <div style={{ padding: '16px', color: 'white', textAlign: 'center', fontSize: '1.2rem', fontWeight: 'bold', letterSpacing: '1px' }}>
        HOME CONTROL
      </div>
      <Menu
        theme="dark"
        mode="inline"
        selectedKeys={[location.pathname]}
        items={menuItems}
        onClick={({ key }) => navigate(key)}
      />
    </Sider>
  );
}