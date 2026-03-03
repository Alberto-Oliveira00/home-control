import { useEffect, useState } from 'react';
import { useUserStore } from '../stores/userStore';
import { Table, Button, Form, Input, InputNumber, Popconfirm, Typography, Space, message, Card, Modal } from 'antd';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import type { User } from '../types';

const { Title } = Typography;

export default function UsersPage() {
  const { users, isLoading, fetchUsers, addUser, editUser, deleteUser } = useUserStore();
  const [form] = Form.useForm();

  const [editForm] = Form.useForm();
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingUser, setEditingUser] = useState<User | null>(null);

  useEffect(() => {
    fetchUsers();
  }, [fetchUsers]);

  const onFinish = async (values: { name: string; age: number }) => {
    await addUser(values.name, values.age);
    form.resetFields();
    message.success('Pessoa adicionada com sucesso!');
  };

  const openEditModal = (user: User) => {
    setEditingUser(user);
    editForm.setFieldsValue({ name: user.name, age: user.age });
    setIsEditModalOpen(true);
  };

  const handleEditSubmit = async () => {
    try {
      const values = await editForm.validateFields();
      if (editingUser) {
        await editUser(editingUser.userId, values.name, values.age);
        message.success('Pessoa atualizada com sucesso!');
        setIsEditModalOpen(false);
        setEditingUser(null);
      }
    } catch (error) {
        message.error('Erro ao editar pessoa');
    }
  };

  const columns = [
    { title: 'ID', dataIndex: 'userId', key: 'userId', width: '10%' },
    { title: 'Nome', dataIndex: 'name', key: 'name' },
    { title: 'Idade', dataIndex: 'age', key: 'age' },
    {
      title: 'Ações',
      key: 'actions',
      render: (_: any, record: any) => (
        <Space>
          <Button 
            type="default" 
            icon={<EditOutlined />} 
            onClick={() => openEditModal(record)}
          >
            Editar
          </Button>
          <Popconfirm
            title="Excluir pessoa"
            description="Tem certeza? Todas as transações serão apagadas!"
            onConfirm={() => deleteUser(record.userId)}
            okText="Sim"
            cancelText="Não"
          >
            <Button danger icon={<DeleteOutlined />}>Excluir</Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <Title level={2}>Gerenciar Pessoas</Title>
      
      <Card style={{ marginBottom: 24 }}>
        <Form form={form} layout="inline" onFinish={onFinish}>
          <Form.Item name="name" rules={[{ required: true, message: 'Informe o nome' }]}>
            <Input placeholder="Nome da pessoa" maxLength={200} style={{ width: 250 }} />
          </Form.Item>
          <Form.Item name="age" rules={[{ required: true, message: 'Informe a idade' }]}>
            <InputNumber placeholder="Idade" min={0} style={{ width: 100 }} />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" loading={isLoading}>
              Adicionar
            </Button>
          </Form.Item>
        </Form>
      </Card>

      <Table 
        dataSource={users} 
        columns={columns} 
        rowKey="userId" 
        loading={isLoading} 
      />

      <Modal
        title="Editar Pessoa"
        open={isEditModalOpen}
        onOk={handleEditSubmit}
        onCancel={() => setIsEditModalOpen(false)}
        okText="Salvar"
        cancelText="Cancelar"
        confirmLoading={isLoading}
      >
        <Form form={editForm} layout="vertical">
          <Form.Item name="name" label="Nome" rules={[{ required: true, message: 'Informe o nome' }]}>
            <Input maxLength={200} />
          </Form.Item>
          <Form.Item name="age" label="Idade" rules={[{ required: true, message: 'Informe a idade' }]}>
            <InputNumber min={0} style={{ width: '100%' }} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}